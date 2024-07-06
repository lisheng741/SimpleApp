using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Simple.Common.EFCore.Data;
using Simple.Common.EFCore.Helpers;
using Simple.Common.EFCore.Models;

namespace Simple.Common.EFCore;

public abstract class SimpleBaseDbContext : DbContext
{
    private readonly IEntityDataProvider _dataProvider;

    protected virtual bool IsMultiTenantFilterEnabled => DataFilter.IsEnabled<IMultiTenant>();
    protected virtual bool IsSoftDeleteFilterEnabled => DataFilter.IsEnabled<ISoftDelete>();

    public DataFilter DataFilter = new DataFilter();

    public SimpleBaseDbContext(DbContextOptions options, IEntityDataProvider dataProvider)
        : base(options)
    {
        _dataProvider = dataProvider;

        Initialize();
    }

    protected virtual void Initialize()
    {
        ChangeTracker.Tracked += ChangeTracker_Tracked;
        ChangeTracker.StateChanged += ChangeTracker_StateChanged;
    }

    protected virtual List<Assembly> GetAssemblies()
    {
        return AssemblyHelper.GetAssemblies();
    }

    #region 实体配置

    protected override void OnModelCreating(ModelBuilder builder)
    {
        // 获取所有数据集
        List<Assembly> assemblies = GetAssemblies();

        // 注册所有实体
        RegisterEntity(builder, assemblies);

        // 配置实体
        ConfigureEntity(builder);

        // 应用用户自定义配置
        ApplyEntityConfigurations(builder, assemblies);
    }

    protected virtual void RegisterEntity(ModelBuilder builder, List<Assembly> assemblies)
    {
        // 获取本程序集内所有继承 EntityBase 的实体
        List<Type> entityTypes = assemblies.SelectMany(s => s.GetTypes())
                                           .Where(t => !t.IsAbstract && t.IsSubclassOf(typeof(IEntity)))
                                           .ToList();

        // 注册 Entity
        foreach (var entityType in entityTypes)
        {
            builder.Entity(entityType);
        }
    }

    protected virtual void ConfigureEntity(ModelBuilder builder)
    {
        // 获取泛型方法 ConfigureEntityQueryFilters<TEntity>(EntityTypeBuilder)
        MethodInfo configureEntityQueryFilterMethodInfo = typeof(SimpleBaseDbContext).GetMethod(
            nameof(ConfigureEntityQueryFilter),
            BindingFlags.Instance | BindingFlags.NonPublic,
            new Type[] { typeof(EntityTypeBuilder) }
        ) ?? throw new NullReferenceException(nameof(configureEntityQueryFilterMethodInfo));

        // 遍历注册的实体进行配置
        foreach (IMutableEntityType mutableEntityType in builder.Model.GetEntityTypes())
        {
            // 如果不是一个独立的实体，则跳过
            if (mutableEntityType.IsOwned())
            {
                continue;
            }

            EntityTypeBuilder entityTypeBuilder = builder.Entity(mutableEntityType.ClrType);

            // Guid 主键默认不自动设置
            if (typeof(EntityBase<Guid>).IsAssignableFrom(mutableEntityType.ClrType))
            {
                PropertyBuilder idPropertyBuilder = entityTypeBuilder.Property(nameof(EntityBase<Guid>.Id));
                if (!idPropertyBuilder.Metadata.PropertyInfo!.IsDefined(typeof(DatabaseGeneratedAttribute), true))
                {
                    idPropertyBuilder.ValueGeneratedNever();
                }
            }

            // 表名注释
            string entitySummary = mutableEntityType.ClrType.GetSummary();
            if (!string.IsNullOrEmpty(entitySummary))
            {
                entityTypeBuilder.ToTable(t => t.HasComment(entitySummary));
            }

            // 尝试配置所有默认属性
            EntityConfigurationHelper.TryConfigureDefaultProperty(entityTypeBuilder);

            // 配置实体属性
            ConfigureEntityProperty(entityTypeBuilder, mutableEntityType);

            // 设置级联删除
            foreach (var foreignKeys in mutableEntityType.GetForeignKeys())
            {
                foreignKeys.DeleteBehavior = DeleteBehavior.Cascade;
            }

            // 调用 ConfigureEntityQueryFilter，配置实体查询筛选器
            configureEntityQueryFilterMethodInfo.MakeGenericMethod(mutableEntityType.ClrType).Invoke(this, new object[] { entityTypeBuilder });
        } // foreach (var entityType in builder.Model.GetEntityTypes())
    }

    protected virtual void ConfigureEntityProperty(EntityTypeBuilder entityTypeBuilder, IMutableEntityType mutableEntityType)
    {
        // 遍历实体属性
        foreach (IMutableProperty mutableProperty in mutableEntityType.GetProperties())
        {
            string propertyName = mutableProperty.Name;
            PropertyBuilder propertyBuilder = entityTypeBuilder.Property(propertyName);

            // 字段注释
            string? propertySummary = mutableProperty.PropertyInfo?.GetSummary();
            if (!string.IsNullOrEmpty(propertySummary))
            {
                propertyBuilder.HasComment(propertySummary);
            }

            ConfigurePropertyGuidAsChar36(propertyBuilder, mutableProperty);
        }
    }

    /// <summary>
    /// Guid 处理为 char(36)
    /// </summary>
    /// <param name="propertyBuilder"></param>
    /// <param name="mutableProperty"></param>
    protected virtual void ConfigurePropertyGuidAsChar36(PropertyBuilder propertyBuilder, IMutableProperty mutableProperty)
    {
        // Guid 处理为 char(36), 这样不论是 SqlServer 还是 MySql 都可以按字符串来处理 Guid，解决数据库排序方式不一致的问题
        if (mutableProperty.ClrType == typeof(Guid))
        {
            propertyBuilder.HasColumnType("char(36)");
        }
        if (mutableProperty.ClrType == typeof(Nullable<Guid>))
        {
            propertyBuilder.HasColumnType("char(36)").IsRequired(false);
        }
    }

    /// <summary>
    /// 配置实体全局查询筛选器
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="entityTypeBuilder"></param>
    protected virtual void ConfigureEntityQueryFilter<TEntity>(EntityTypeBuilder entityTypeBuilder)
        where TEntity : class
    {
        // 生成筛选器需要的表达式树
        Expression<Func<TEntity, bool>>? expression = CreateFilterExpression<TEntity>();

        // 如果没有生成表达式树，则直接返回
        if (expression == null) return;

        // 设置实体的筛选器（注：筛选器只会生效最后一个）
        entityTypeBuilder.HasQueryFilter(expression);
    }

    protected virtual Expression<Func<TEntity, bool>>? CreateFilterExpression<TEntity>()
        where TEntity : class
    {
        Expression<Func<TEntity, bool>>? expression = null;

        // 如果实现了软删接口
        if (typeof(ISoftDelete).IsAssignableFrom(typeof(TEntity)))
        {
            expression = e => !IsSoftDeleteFilterEnabled || !EF.Property<bool>(e, nameof(ISoftDelete.IsDeleted));
        }

        // 多租户
        if (typeof(IMultiTenant).IsAssignableFrom(typeof(TEntity)))
        {
            Expression<Func<TEntity, bool>> tenantExpression = e => EF.Property<Guid>(e, nameof(IMultiTenant.TenantId)) == _dataProvider.TenantId;
            expression = ExpressionHelper.Combine(expression, tenantExpression, ExpressionCombineType.AndAlso);
        }

        return expression;
    }

    protected virtual void ApplyEntityConfigurations(ModelBuilder builder, List<Assembly> assemblies)
    {
        // 应用 IEntityConfiguration 的自定义配置
        EntityConfigurationHelper.ApplyEntityConfigurations(builder, assemblies.ToArray());

        // 应用 IEntityTypeConfiguration<> 的专有配置
        foreach (Assembly assembly in assemblies)
        {
            builder.ApplyConfigurationsFromAssembly(assembly);
        }
    }

    #endregion

    #region 实体状态跟踪

    private void ChangeTracker_Tracked(object? sender, EntityTrackedEventArgs e)
    {
        TrackedEntityEntry(e.Entry);
    }

    private void ChangeTracker_StateChanged(object? sender, EntityStateChangedEventArgs e)
    {
        TrackedEntityEntry(e.Entry);
    }

    private void TrackedEntityEntry(EntityEntry entry)
    {
        switch (entry.State)
        {
            case EntityState.Added:
                EntityStateAdded(entry);
                break;
            case EntityState.Modified:
                EntityStateModified(entry);
                break;
            case EntityState.Deleted:
                EntityStateDeleted(entry);
                break;
        }
    }

    protected virtual void EntityStateAdded(EntityEntry entry)
    {
        // 主键检查及设置
        if (entry.Entity is EntityBase<Guid> entityWithGuidId)
        {
            if (entityWithGuidId.Id == default)
            {
                Guid id = GuidHelper.Next();
                entry.Property(nameof(EntityBase<Guid>.Id)).CurrentValue = id;
            }
        }

        // 并发控制字段检查（ConcurrencyCheck）
        if (entry.Entity is IConcurrency concurrency)
        {
            if (concurrency.RowVersion == default)
            {
                concurrency.RowVersion = DateTimeOffset.UtcNow.Ticks;
            }
        }

        // 字段自动赋值
        if (entry.Entity is ICreatedTime createdTime)
        {
            if (createdTime.CreatedTime == default)
            {
                createdTime.CreatedTime = _dataProvider.GetCreatedTime();
            }
        }
        if (entry.Entity is ICreatedUser createdUser)
        {
            if (createdUser.CreatedUserId == default)
            {
                createdUser.CreatedUserId = _dataProvider.GetCreatedUser();
            }
        }

        _dataProvider.HandleEntityStateAdded(entry);
    }

    protected virtual void EntityStateModified(EntityEntry entry)
    {
        // 字段自动赋值
        if (entry.Entity is IConcurrency concurrency)
        {
            concurrency.RowVersion = DateTimeOffset.UtcNow.Ticks;
        }
        if (entry.Entity is IUpdatedTime updatedTime)
        {
            updatedTime.UpdatedTime = _dataProvider.GetUpdatedTime();
        }
        if (entry.Entity is IUpdatedUser updatedUser)
        {
            updatedUser.UpdatedUserId = _dataProvider.GetUpdatedUser();
        }

        _dataProvider.HandleEntityStateModified(entry);
    }

    protected virtual void EntityStateDeleted(EntityEntry entry)
    {
        // 并发字段赋值
        if (entry.Entity is IConcurrency concurrency)
        {
            concurrency.RowVersion = DateTimeOffset.UtcNow.Ticks;
        }

        EntitySoftDelete(entry);

        _dataProvider.HandleEntityStateDeleted(entry);
    }

    protected virtual void EntitySoftDelete(EntityEntry entry)
    {
        // 如果没有实现软删接口则直接返回，正常硬删
        if (!(entry.Entity is ISoftDelete softDelete))
        {
            return;
        }

        // 如果实现了软删，又想要硬删
        if (!IsSoftDeleteFilterEnabled)
        {
            return;
        }

        //// 从数据库重载实体，这样做有个问题，每删一个实体访问一次数据库，效率得不到保证
        //entry.Reload();

        // 设置软删字段
        softDelete.IsDeleted = true;

        // 先将状态置为修改（会触发修改的事件）
        entry.State = EntityState.Modified;

        // 遍历实体的属性，除了需要更新的属性外，其他全将状态设置为未修改。这样生成的 SQL 语句，只会更新相应的的字段
        IEnumerable<PropertyEntry> properties = entry.Properties;
        foreach (var property in properties)
        {
            string name = property.Metadata.Name;

            if (name == nameof(ISoftDelete.IsDeleted)) continue;
            if (entry.Entity is IUpdatedTime && name == nameof(IUpdatedTime.UpdatedTime)) continue;
            if (entry.Entity is IUpdatedUser && name == nameof(IUpdatedUser.UpdatedUserId)) continue;

            property.IsModified = false;
        }
    }

    #endregion
}
