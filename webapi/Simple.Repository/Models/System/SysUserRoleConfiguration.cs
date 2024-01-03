namespace Simple.Repository.Models.System;

public class SysUserRoleConfiguration : IEntityTypeConfiguration<SysUserRole>
{
    public void Configure(EntityTypeBuilder<SysUserRole> builder)
    {
        // 复合主键设置
        builder.HasKey(e => new { e.UserId, e.RoleId });

        // 索引设置
        builder.HasIndex(e => new { e.RoleId })
               .IsUnique(false);
    }
}
