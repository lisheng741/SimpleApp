namespace Simple.Repository.Models.System;

/// <summary>
/// 组织表
/// </summary>
public class SysOrganization : BusinessEntityBase<Guid>
{
    /// <summary>
    /// 父级Id
    /// </summary>
    public Guid ParentId { get; set; }

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
    /// 层级
    /// </summary>
    public int Level { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    public int Sort { get; set; }

    /// <summary>
    /// 启用状态
    /// </summary>
    public bool IsEnabled { get; set; } = true;

    public SysOrganization(string code, string name)
    {
        Code = code;
        Name = name;
    }
}
