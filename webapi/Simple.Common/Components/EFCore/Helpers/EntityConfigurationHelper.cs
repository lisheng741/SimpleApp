using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Simple.Common.EFCore.Models;

namespace Simple.Common.EFCore.Helpers;

public static class EntityConfigurationHelper
{
    /// <summary>
    /// 应用实现 IEntityConfiguration 接口的配置。
    /// 一般发生在默认配置之后，ApplyConfigurationsFromAssembly 之前。
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="assemblies"></param>
    public static void ApplyEntityConfigurations(ModelBuilder builder, params Assembly[] assemblies)
    {
        List<Type> types = assemblies.SelectMany(a => a.GetTypes())
                                     .Where(t => !t.IsAbstract && t.IsAssignableTo(typeof(IEntityConfiguration)))
                                     .ToList();

        foreach (var type in types)
        {
            // 只接受无参构造函数
            // Only accpet types that contain a parameterless constructor
            if (type.GetConstructor(Type.EmptyTypes) == null)
            {
                continue;
            }

            // 实例化
            var configuration = Activator.CreateInstance(type) as IEntityConfiguration;
            if (configuration != null)
            {
                configuration.Configure(builder);
            }
        }
    }

    public static void ApplyEntityConfiguration(ModelBuilder builder, IEntityConfiguration configuration)
    {
        configuration.Configure(builder);
    }

    /// <summary>
    /// 尝试配置所有默认属性
    /// </summary>
    /// <param name="builder"></param>
    public static void TryConfigureDefaultProperty(EntityTypeBuilder builder)
    {
        TryConfigureTenant(builder);
        TryConfigureSoftDelete(builder);
        TryConfigureCreatedTime(builder);
        TryConfigureCreatedUser(builder);
        TryConfigureUpdatedTime(builder);
        TryConfigureUpdatedUser(builder);
        TryConfigureConcurrency(builder);
        TryConfigureRowVersion(builder);
    }

    public static void TryConfigureTenant(EntityTypeBuilder builder)
    {
        if (builder.Metadata.ClrType.IsAssignableTo(typeof(IMultiTenant)))
        {
            builder.Property(nameof(IMultiTenant.TenantId))
                   .IsRequired(false);
        }
    }

    public static void TryConfigureSoftDelete(EntityTypeBuilder builder)
    {
        if (builder.Metadata.ClrType.IsAssignableTo(typeof(ISoftDelete)))
        {
            builder.Property(nameof(ISoftDelete.IsDeleted))
                   .IsRequired()
                   .HasDefaultValue(false);
        }
    }

    public static void TryConfigureCreatedTime(EntityTypeBuilder builder)
    {
        if (builder.Metadata.ClrType.IsAssignableTo(typeof(ICreatedTime)))
        {
            builder.Property(nameof(ICreatedTime.CreatedTime))
                   .IsRequired(false);
        }
    }

    public static void TryConfigureCreatedUser(EntityTypeBuilder builder)
    {
        if (builder.Metadata.ClrType.IsAssignableTo(typeof(ICreatedUser)))
        {
            builder.Property(nameof(ICreatedUser.CreatedUserId))
                   .IsRequired(false);
        }
    }

    public static void TryConfigureUpdatedTime(EntityTypeBuilder builder)
    {
        if (builder.Metadata.ClrType.IsAssignableTo(typeof(IUpdatedTime)))
        {
            builder.Property(nameof(IUpdatedTime.UpdatedTime))
                   .IsRequired(false);
        }
    }

    public static void TryConfigureUpdatedUser(EntityTypeBuilder builder)
    {
        if (builder.Metadata.ClrType.IsAssignableTo(typeof(IUpdatedUser)))
        {
            builder.Property(nameof(IUpdatedUser.UpdatedUserId))
                   .IsRequired(false);
        }
    }

    public static void TryConfigureConcurrency(EntityTypeBuilder builder)
    {
        if (builder.Metadata.ClrType.IsAssignableTo(typeof(IConcurrency)))
        {
            builder.Property(nameof(IConcurrency.RowVersion))
                   .IsRequired()
                   .IsConcurrencyToken();
        }
    }

    public static void TryConfigureRowVersion(EntityTypeBuilder builder)
    {
        if (builder.Metadata.ClrType.IsAssignableTo(typeof(IRowVersion)))
        {
            builder.Property(nameof(IRowVersion.RowVersion))
                   .IsRequired()
                   .IsRowVersion();
        }
    }
}
