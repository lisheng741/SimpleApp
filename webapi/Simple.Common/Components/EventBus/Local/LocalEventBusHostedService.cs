using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Simple.Common.EventBus.Local;

public class LocalEventBusHostedService : BackgroundService
{
    private readonly ILogger<LocalEventBusHostedService> _logger;
    private readonly ISubscribeManager _subscribeExecuter;
    private readonly IEventStore _queue;

    public LocalEventBusHostedService(ILogger<LocalEventBusHostedService> logger,
                                      ISubscribeManager subscribeExecuter,
                                      IEventStore queue)
    {
        _logger = logger;
        _subscribeExecuter = subscribeExecuter;
        _queue = queue;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation($"{nameof(LocalEventBusHostedService)} is running.");

        return ProcessTaskQueueAsync(stoppingToken);
    }

    private async Task ProcessTaskQueueAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            // 从队列中读取事件消息
            object @event = await _queue.ReadAsync(stoppingToken);

            // 处理事件
            await _subscribeExecuter.ProcessEvent(@event);
        }
    }

    public override async Task StopAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation($"{nameof(LocalEventBusHostedService)} is stopping.");

        await base.StopAsync(stoppingToken);
    }

    public override void Dispose()
    {
        _logger.LogInformation($"{nameof(LocalEventBusHostedService)} is dispose.");

        base.Dispose();
    }
}
