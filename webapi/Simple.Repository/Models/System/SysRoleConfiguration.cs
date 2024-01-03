namespace Simple.Repository.Models.System;

public class SysRoleConfiguration : IEntityTypeConfiguration<SysRole>
{
    public void Configure(EntityTypeBuilder<SysRole> builder)
    {
        // DataScope 默认值 1
        builder.Property(r => r.DataScope).HasDefaultValue(DataScopeType.All);
    }
}
