namespace Simple.Repository.Models.System;

/// <summary>
/// 用户角色关联表
/// </summary>
public class SysUserRole : EntityBase
{
    /// <summary>
    /// 用户Id
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// 角色Id
    /// </summary>
    public Guid RoleId { get; set; }

    /// <summary>
    /// 用户
    /// </summary>
    public SysUser? User { get; set; }

    /// <summary>
    /// 角色
    /// </summary>
    public SysRole? Role { get; set; }

    public override void ConfigureEntity(ModelBuilder builder)
    {
        // 复合主键设置
        builder.Entity<SysUserRole>()
            .HasKey(e => new { e.UserId, e.RoleId });

        // 索引设置
        builder.Entity<SysUserRole>()
            .HasIndex(e => new { e.RoleId, e.UserId })
            .IsUnique(false);
    }
}
