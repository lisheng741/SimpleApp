using System.Runtime;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Quartz;
using Simple.Common.Quartz.Const;
using Simple.Common.Quartz.Models;

namespace Simple.Common.Quartz;

public class QuartzManager : IQuartzManager
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ISchedulerFactory _schedulerFactory;
    private readonly ILogger<QuartzManager> _logger;
    private readonly JobSchedulingOptions _options;

    public QuartzManager(IServiceProvider serviceProvider,
                         ISchedulerFactory schedulerFactory,
                         ILogger<QuartzManager> logger,
                         IOptions<JobSchedulingOptions> options)
    {
        _serviceProvider = serviceProvider;
        _schedulerFactory = schedulerFactory;
        _logger = logger;
        _options = options.Value;
    }

    public async Task<IScheduler> GetScheduler(CancellationToken token = default)
    {
        return await _schedulerFactory.GetScheduler(token);
    }

    public async Task Start(CancellationToken token = default)
    {
        var scheduler = await GetScheduler();

        _logger.LogInformation($"Job scheduling start.");

        await scheduler.Start(token);

        using var scope = _serviceProvider.CreateScope();
        var serviceProvider = scope.ServiceProvider;
        if (_options.StartHandle != null) await _options.StartHandle(serviceProvider);
    }

    public async Task Shutdown(CancellationToken token = default)
    {
        var scheduler = await GetScheduler();

        _logger.LogInformation($"Job scheduling shutdown.");

        await scheduler.Shutdown(token);

        using var scope = _serviceProvider.CreateScope();
        var serviceProvider = scope.ServiceProvider;
        if (_options.ShutdownHandle != null) await _options.ShutdownHandle(serviceProvider);
    }

    public async Task<bool> CheckExists(string jobName, CancellationToken token = default)
    {
        var scheduler = await GetScheduler();
        var jobKey = new JobKey(jobName);
        return await scheduler.CheckExists(jobKey, token);
    }

    public Task AddJob<T>(JobInfo jobInfo, CancellationToken token = default)
        where T : IJob
    {
        return AddJob(typeof(T), jobInfo, token);
    }

    public async Task AddJob(Type jobType, JobInfo jobInfo, CancellationToken token = default)
    {
        var scheduler = await GetScheduler();

        _logger.LogInformation($"AddJob: {JsonHelper.Serialize(jobInfo)}");

        var jobKey = new JobKey(jobInfo.Name);
        var job = JobBuilder.Create(jobType)
                        .WithIdentity(jobKey)
                        .UsingJobData(JobSchedulingConst.JobNameKey, jobInfo.Name)
                        .Build();

        var triggers = new List<ITrigger>();
        foreach (var triggerInfo in jobInfo.Triggers)
        {
            var trigger = TriggerBuilder.Create()
                .ForJob(jobKey)
                .WithIdentity(triggerInfo.Name)
                .WithCronSchedule(triggerInfo.Cron)
                .Build();
            triggers.Add(trigger);
        }

        await scheduler.ScheduleJob(job, triggers, true, token);
    }

    public async Task<bool> DeleteJob(string jobName, CancellationToken token = default)
    {
        var scheduler = await GetScheduler();
        
        _logger.LogInformation($"DeleteJob {jobName}");

        var jobKey = new JobKey(jobName);
        return await scheduler.DeleteJob(jobKey, token);
    }

    public async Task PauseJob(string jobName, CancellationToken token = default)
    {
        var scheduler = await GetScheduler();

        _logger.LogInformation($"PauseJob {jobName}");

        var jobKey = new JobKey(jobName);
        await scheduler.PauseJob(jobKey, token);
    }

    public async Task ResumeJob(string jobName, CancellationToken token = default)
    {
        var scheduler = await GetScheduler();

        _logger.LogInformation($"ResumeJob {jobName}");

        var jobKey = new JobKey(jobName);
        await scheduler.ResumeJob(jobKey, token);
    }
}
