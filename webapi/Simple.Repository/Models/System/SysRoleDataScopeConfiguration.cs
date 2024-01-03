namespace Simple.Repository.Models.System;

public class SysRoleDataScopeConfiguration : IEntityTypeConfiguration<SysRoleDataScope>
{
    public void Configure(EntityTypeBuilder<SysRoleDataScope> builder)
    {
        // 复合主键
        builder.HasKey(e => new { e.RoleId, e.OrganizationId });

        // Index
        builder.HasIndex(e => new { e.OrganizationId })
               .IsUnique(false);

        // 关系配置
        builder.HasOne(rd => rd.Role)
               .WithMany(r => r.RoleDataScopes)
               .IsRequired();
        builder.HasOne(rd => rd.Organization)
               .WithMany()
               .IsRequired();
    }
}
