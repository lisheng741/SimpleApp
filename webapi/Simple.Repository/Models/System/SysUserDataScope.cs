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
}
