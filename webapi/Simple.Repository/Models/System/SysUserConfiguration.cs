namespace Simple.Repository.Models.System;

public class SysUserConfiguration : IEntityTypeConfiguration<SysUser>
{
    public void Configure(EntityTypeBuilder<SysUser> builder)
    {
        // 配置关系：组织/岗位的删除，不能影响用户表
        builder.HasOne(u => u.Organization)
               .WithMany()
               .OnDelete(DeleteBehavior.SetNull);
        builder.HasOne(u => u.Position)
               .WithMany()
               .OnDelete(DeleteBehavior.SetNull);

        // AdminType 默认值 3
        builder.Property(r => r.AdminType).HasDefaultValue(AdminType.None);
    }
}
