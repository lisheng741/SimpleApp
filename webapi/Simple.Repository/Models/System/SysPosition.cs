namespace Simple.Repository.Models.System;

/// <summary>
/// 岗位表
/// </summary>
public class SysPosition : BusinessEntityBase<Guid>
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

    public SysPosition(string code, string name)
    {
        Code = code;
        Name = name;
    }
}
