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
}
