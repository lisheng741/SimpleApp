namespace Simple.Repository.Models.System;

/// <summary>
/// 角色表
/// </summary>
public class SysRole : BusinessEntityBase<Guid>, IConcurrency
{
    /// <summary>
    /// 编码
    /// </summary>
    [MaxLength(128)]
    public string Code { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    [MaxLength(128)]
    public string Name { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    public int Sort { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [MaxLength(2048)]
    public string? Remark { get; set; }

    /// <summary>
    /// 启用状态
    /// </summary>
    public bool IsEnabled { get; set; } = true;

    /// <summary>
    /// 用户角色
    /// </summary>
    public List<SysUserRole> UserRoles { get; set; } = new List<SysUserRole>();

    public long RowVersion { get; set; }

    public SysRole(string code, string name)
    {
        Code = code;
        Name = name;
    }
}
