using Quartz;
using Simple.Common.Quartz;

namespace Microsoft.Extensions.DependencyInjection;

public static class JobSchedulingServiceCollectionExtensions
{
    /// <summary>
    /// 添加定时任务相关服务（基于 Quartz 实现）
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddJobScheduling(this IServiceCollection services, Action<JobSchedulingOptions>? setupAction = null)
    {
        services.AddQuartz(config =>
        {
            config.UseMicrosoftDependencyInjectionJobFactory();
        });

        services.AddHostedService<QuartzHostedService>();
        services.AddSingleton<IQuartzManager, QuartzManager>();

        if (setupAction != null) services.Configure(setupAction);

        return services;
    }
}
