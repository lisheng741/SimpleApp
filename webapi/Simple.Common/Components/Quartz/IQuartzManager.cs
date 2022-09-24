using Quartz;
using Simple.Common.Quartz.Models;

namespace Simple.Common.Quartz;

public interface IQuartzManager
{
    Task<IScheduler> GetScheduler(CancellationToken token = default);

    Task Start(CancellationToken token = default);

    Task Shutdown(CancellationToken token = default);

    Task<bool> CheckExists(string jobName, CancellationToken token = default);

    Task AddJob<T>(JobInfo jobInfo, CancellationToken token = default)
        where T : IJob;

    Task AddJob(Type jobType, JobInfo jobInfo, CancellationToken token = default);

    Task<bool> DeleteJob(string jobName, CancellationToken token = default);

    Task PauseJob(string jobName, CancellationToken token = default);

    Task ResumeJob(string jobName, CancellationToken token = default);
}
