namespace Simple.Repository.Models.System;

/// <summary>
/// 角色菜单关联表
/// </summary>
public class SysRoleMenu : EntityBase
{
    /// <summary>
    /// 角色Id
    /// </summary>
    public Guid RoleId { get; set; }

    /// <summary>
    /// 菜单Id
    /// </summary>
    public Guid MenuId { get; set; }

    /// <summary>
    /// 角色
    /// </summary>
    public SysRole? Role { get; set; }

    /// <summary>
    /// 菜单
    /// </summary>
    public SysMenu? Menu { get; set; }
}
