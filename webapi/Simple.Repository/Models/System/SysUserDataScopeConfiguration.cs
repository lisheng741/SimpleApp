namespace Simple.Repository.Models.System;

public class SysUserDataScopeConfiguration : IEntityTypeConfiguration<SysUserDataScope>
{
    public void Configure(EntityTypeBuilder<SysUserDataScope> builder)
    {
        // 复合主键
        builder.HasKey(e => new { e.UserId, e.OrganizationId });

        // Index
        builder.HasIndex(e => new { e.OrganizationId })
               .IsUnique(false);

        // 关系配置
        builder.HasOne(ud => ud.User)
               .WithMany(r => r.UserDataScopes)
               .IsRequired();
        builder.HasOne(rm => rm.Organization)
               .WithMany()
               .IsRequired();
    }
}
