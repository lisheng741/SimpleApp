using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Simple.Repository.Data;

public class SimpleDbContext : DbContext, ISimpleDbContext
{
    public SimpleDbContext(DbContextOptions<SimpleDbContext> options)
        : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        List<Type> entityTypes = Assembly.GetExecutingAssembly().GetTypes()
                                        .Where(t => !t.IsAbstract && t.IsSubclassOf(typeof(EntityBase)))
                                        .ToList();

        // 注册 Entity
        foreach (var entityType in entityTypes)
        {
            EntityTypeBuilder entityTypeBuilder = builder.Entity(entityType);

            // 注释
            FieldInfo[] fieldInfos = entityType.GetFields();
            foreach (var fieldInfo in fieldInfos)
            {
            }
        }
    }
}
