namespace Simple.Repository.Models.System;

public class SysUserDataScope : EntityBase
{

    /// <summary>
    /// 用户Id
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// 组织Id
    /// </summary>
    public Guid OrganizationId { get; set; }

    /// <summary>
    /// 用户
    /// </summary>
    public SysUser? User { get; set; }

    /// <summary>
    /// 组织
    /// </summary>
    public SysOrganization? Organization { get; set; }

    public override void ConfigureEntity(ModelBuilder builder)
    {
        // 复合主键
        builder.Entity<SysUserDataScope>()
            .HasKey(e => new { e.UserId, e.OrganizationId });

        // Index
        builder.Entity<SysUserDataScope>()
            .HasIndex(e => new { e.OrganizationId, e.UserId })
            .IsUnique(false);

        // 关系配置
        builder.Entity<SysUserDataScope>()
            .HasOne(ud => ud.User)
            .WithMany(r => r.UserDataScopes)
            .IsRequired();
        builder.Entity<SysUserDataScope>()
            .HasOne(rm => rm.Organization)
            .WithMany()
            .IsRequired();
    }
}
