using Microsoft.Extensions.Logging;
using Quartz;
using Simple.Common.Quartz.Const;

namespace Simple.Services.Jobs;

[DisallowConcurrentExecution]
public class TestJob : IJob
{
    private readonly ILogger<TestJob> _logger;

    public TestJob(ILogger<TestJob> logger)
    {
        _logger = logger;
    }

    public Task Execute(IJobExecutionContext context)
    {
        var dataMap = context.JobDetail.JobDataMap;
        var jobName = dataMap.GetString(JobSchedulingConst.JobNameKey);

        try
        {
            Console.WriteLine($"{jobName} run at {DateTimeOffset.Now}");
        }
        catch(Exception ex)
        {
            _logger.LogError($"{jobName} 发生错误: {ex.Message}");
        }

        return Task.CompletedTask;
    }
}
