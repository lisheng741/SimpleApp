namespace Simple.Services;

public class JobPageInputModel: PageInputModel
{
    /// <summary>
    /// 任务名称
    /// </summary>
    public string? TimerName { get; set; }

    /// <summary>
    /// 任务状态（1-运行，2-暂停）
    /// </summary>
    public int? JobStatus { get; set; }
}
