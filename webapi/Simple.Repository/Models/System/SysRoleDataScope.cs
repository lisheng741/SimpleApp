namespace Simple.Repository.Models.System;

public class SysRoleDataScope : EntityBase
{
    /// <summary>
    /// 角色Id
    /// </summary>
    public Guid RoleId { get; set; }

    /// <summary>
    /// 组织Id
    /// </summary>
    public Guid OrganizationId { get; set; }

    /// <summary>
    /// 角色
    /// </summary>
    public SysRole? Role { get; set; }

    /// <summary>
    /// 组织
    /// </summary>
    public SysOrganization? Organization { get; set; }

    public override void ConfigureEntity(ModelBuilder builder)
    {
        // 复合主键
        builder.Entity<SysRoleDataScope>()
            .HasKey(e => new { e.RoleId, e.OrganizationId });

        // Index
        builder.Entity<SysRoleDataScope>()
            .HasIndex(e => new { e.OrganizationId, e.RoleId })
            .IsUnique(false);

        // 关系配置
        builder.Entity<SysRoleDataScope>()
            .HasOne(rd => rd.Role)
            .WithMany(r => r.RoleDataScopes)
            .IsRequired();
        builder.Entity<SysRoleDataScope>()
            .HasOne(rd => rd.Organization)
            .WithMany()
            .IsRequired();
    }
}
