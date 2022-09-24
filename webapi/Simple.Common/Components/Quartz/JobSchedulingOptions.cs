namespace Simple.Common.Quartz;

public class JobSchedulingOptions
{
    /// <summary>
    /// 定时任务启动时执行的程序
    /// </summary>
    public Func<IServiceProvider, Task>? StartHandle { get; set; }

    /// <summary>
    /// 定时任务关闭时执行的程序
    /// </summary>
    public Func<IServiceProvider, Task>? ShutdownHandle { get; set; }
}
