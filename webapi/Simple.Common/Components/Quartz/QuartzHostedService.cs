using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Simple.Common.Quartz;

public class QuartzHostedService : IHostedService
{
    private readonly IQuartzManager _quartzManager;

    public QuartzHostedService(IQuartzManager quartzManager)
    {
        _quartzManager = quartzManager;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await _quartzManager.Start();
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await _quartzManager.Shutdown();
    }
}
