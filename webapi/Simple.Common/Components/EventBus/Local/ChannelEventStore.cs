using Microsoft.Extensions.Options;
using System.Threading.Channels;

namespace Simple.Common.EventBus.Local;

/// <summary>
/// 基于 Channel 的队列存储器
/// </summary>
public class ChannelEventStore : IEventStore
{
    private readonly Channel<object> _queue;

    public ChannelEventStore(IOptions<LocalEventBusOptions> options)
    {
        // 通道容量
        int capacity = options.Value.Capacity;

        // 配置通道，设置超出默认容量后进入等待
        BoundedChannelOptions channelOptions = new BoundedChannelOptions(capacity)
        {
            FullMode = BoundedChannelFullMode.Wait
        };

        // 创建具有最大容量的通道
        _queue = Channel.CreateBounded<object>(channelOptions);
    }

    public async ValueTask WriteAsync<TEvent>(TEvent @event)
        where TEvent : class, IEventModel
    {
        if(@event is null)
        {
            throw new ArgumentNullException(nameof(@event));
        }

        await _queue.Writer.WriteAsync(@event);
    }

    public async ValueTask<object> ReadAsync(CancellationToken cancellationToken)
    {
        return await _queue.Reader.ReadAsync(cancellationToken);
    }
}
