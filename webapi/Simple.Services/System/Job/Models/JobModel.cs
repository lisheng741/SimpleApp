namespace Simple.Services;

public class JobModel : ModelBase
{
    /// <summary>
    /// 主键
    /// </summary>
    public Guid? Id { get; set; }

    /// <summary>
    /// 任务名称
    /// </summary>
    public string TimerName { get; set; } = "";

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
    /// 任务状态（1-运行，2-暂停）
    /// </summary>
    public int JobStatus { get; set; } = 1;


    public override void ConfigureMapper(Profile profile)
    {
        profile.CreateMap<SysJob, JobModel>()
            .ForMember(d => d.TimerName, options => options.MapFrom(s => s.Name))
            .ForMember(d => d.JobStatus, options => options.MapFrom(s => s.IsEnabled ? 1 : 2));

        profile.CreateMap<JobModel, SysJob>()
            .ForMember(d => d.Id, options => options.Ignore())
            .ForMember(d => d.Name, options => options.MapFrom(s => s.TimerName))
            .ForMember(d => d.IsEnabled, options => options.MapFrom(s => s.JobStatus == 1));
    }
}
