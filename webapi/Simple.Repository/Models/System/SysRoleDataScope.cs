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
}
