using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.EntityFrameworkCore.Metadata.Builders;

public static class EntityTypeBuilderExtensions
{
    /// <summary>
    /// 尝试配置默认属性
    /// </summary>
    /// <param name="builder"></param>
    public static void TryConfigureDefault(this EntityTypeBuilder builder)
    {
        builder.TryConfigureTenant();
        builder.TryConfigureSoftDelete();
        builder.TryConfigureCreatedTime();
        builder.TryConfigureCreatedUser();
        builder.TryConfigureUpdatedTime();
        builder.TryConfigureUpdatedUser();
        builder.TryConfigureConcurrency();
        builder.TryConfigureRowVersion();
    }

    public static void TryConfigureTenant(this EntityTypeBuilder builder)
    {
        if (builder.Metadata.ClrType.IsAssignableTo(typeof(ITenant)))
        {
            builder.Property(nameof(ITenant.TenantId))
                .IsRequired(false);
        }
    }

    public static void TryConfigureSoftDelete(this EntityTypeBuilder builder)
    {
        if(builder.Metadata.ClrType.IsAssignableTo(typeof(ISoftDelete)))
        {
            builder.Property(nameof(ISoftDelete.IsDeleted))
                .IsRequired()
                .HasDefaultValue(false);
        }
    }

    public static void TryConfigureCreatedTime(this EntityTypeBuilder builder)
    {
        if (builder.Metadata.ClrType.IsAssignableTo(typeof(ICreatedTime)))
        {
            builder.Property(nameof(ICreatedTime.CreatedTime))
                .IsRequired(false);
        }
    }

    public static void TryConfigureCreatedUser(this EntityTypeBuilder builder)
    {
        if (builder.Metadata.ClrType.IsAssignableTo(typeof(ICreatedUser)))
        {
            builder.Property(nameof(ICreatedUser.CreatedUserId))
                .IsRequired(false);
        }
    }

    public static void TryConfigureUpdatedTime(this EntityTypeBuilder builder)
    {
        if (builder.Metadata.ClrType.IsAssignableTo(typeof(IUpdatedTime)))
        {
            builder.Property(nameof(IUpdatedTime.UpdatedTime))
                .IsRequired(false);
        }
    }

    public static void TryConfigureUpdatedUser(this EntityTypeBuilder builder)
    {
        if (builder.Metadata.ClrType.IsAssignableTo(typeof(IUpdatedUser)))
        {
            builder.Property(nameof(IUpdatedUser.UpdatedUserId))
                .IsRequired(false);
        }
    }

    public static void TryConfigureConcurrency(this EntityTypeBuilder builder)
    {
        if (builder.Metadata.ClrType.IsAssignableTo(typeof(IConcurrency)))
        {
            builder.Property(nameof(IConcurrency.RowVersion))
                .IsRequired()
                .IsConcurrencyToken();
        }
    }

    public static void TryConfigureRowVersion(this EntityTypeBuilder builder)
    {
        if(builder.Metadata.ClrType.IsAssignableTo(typeof(IRowVersion)))
        {
            builder.Property(nameof(IRowVersion.RowVersion))
                .IsRequired()
                .IsRowVersion();
        }
    }
}
