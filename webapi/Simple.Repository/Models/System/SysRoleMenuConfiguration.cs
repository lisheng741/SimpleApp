namespace Simple.Repository.Models.System;

public class SysRoleMenuConfiguration : IEntityTypeConfiguration<SysRoleMenu>
{
    public void Configure(EntityTypeBuilder<SysRoleMenu> builder)
    {
        // 复合主键
        builder.HasKey(e => new { e.RoleId, e.MenuId });

        // Index
        builder.HasIndex(e => new { e.MenuId })
               .IsUnique(false);
    }
}
