namespace Simple.Repository.Models.System;

/// <summary>
/// 应用表
/// </summary>
public class SysApplication : BusinessEntityBase<Guid>
{
    /// <summary>
    /// 编码
    /// </summary>
    [MaxLength(128)]
    public string Code { get; set; } = "";

    /// <summary>
    /// 名称
    /// </summary>
    [MaxLength(128)]
    public string Name { get; set; } = "";

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
    /// 是否激活（只能同时激活一个应用）。
    /// </summary>
    public bool IsActive { get; set; } = false;
}
