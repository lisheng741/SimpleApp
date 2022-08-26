namespace Simple.Repository.Models.System;

/// <summary>
/// 字典子项表
/// </summary>
public class SysDictionaryItem : EntityBase<Guid>
{
    /// <summary>
    /// 字典Id
    /// </summary>
    public Guid DictionaryId { get; set; }

    /// <summary>
    /// 编码
    /// </summary>
    [MaxLength(128)]
    public string Code { get; set; } = "";

    /// <summary>
    /// 显示名称
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
    /// 字典
    /// </summary>
    public SysDictionary Dictionary { get; set; } = default!;

    public override void ConfigureEntity(ModelBuilder builder)
    {
    }
}
