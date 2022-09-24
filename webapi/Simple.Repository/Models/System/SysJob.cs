namespace Simple.Repository.Models.System;

public class SysJob : EntityBase<Guid>
{
    /// <summary>
    /// 任务名称
    /// </summary>
    public string Name { get; set; } = "";

    /// <summary>
    /// 任务类名（完整名称）
    /// </summary>
    public string ActionClass { get; set; } = "";

    /// <summary>
    /// Cron表达式
    /// </summary>
    public string Cron { get; set; } = "";

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
}
