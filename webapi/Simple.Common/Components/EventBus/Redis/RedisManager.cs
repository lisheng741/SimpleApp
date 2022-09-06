using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace Simple.Common.EventBus.Redis;

public class RedisManager : IRedisManager, IDisposable
{
    private readonly ILogger<RedisManager> _logger;
    private readonly ISubscribeManager _subscribeExecuter;
    private readonly RedisEventBusOptions _options;

    private volatile IConnectionMultiplexer? _connection;
    private ISubscriber? _subscriber;
    private bool _disposed;
    private bool _started;
    private readonly SemaphoreSlim _connectionLock = new SemaphoreSlim(1, 1);
    private readonly SemaphoreSlim _startLock = new SemaphoreSlim(1, 1);

    public IConnectionMultiplexer Connection => _connection ?? throw new NullReferenceException(nameof(Connection));

    public ISubscriber Subscriber => _subscriber ?? throw new NullReferenceException(nameof(Subscriber));

    public RedisManager(ILogger<RedisManager> logger,
                        ISubscribeManager subscribeExecuter,
                        IOptions<RedisEventBusOptions> options)
	{
        _logger = logger;
        _subscribeExecuter = subscribeExecuter;
        _options = options.Value;
    }

    private void Connect()
    {
        CheckDisposed();
        if(_subscriber != null)
        {
            return;
        }

        _connectionLock.Wait();
        try
        {
            if(_subscriber == null)
            {
                if(_options.ConnectionMultiplexerFactory == null)
                {
                    if (_options.ConfigurationOptions is not null)
                    {
                        _connection = ConnectionMultiplexer.Connect(_options.ConfigurationOptions);
                    }
                    else
                    {
                        _connection = ConnectionMultiplexer.Connect(_options.Configuration);
                    }
                }
                else
                {
                    _connection = _options.ConnectionMultiplexerFactory().GetAwaiter().GetResult();
                }

                _subscriber = _connection.GetSubscriber();
            }
        }
        finally
        {
            _connectionLock.Release();
        }
    }

    private async Task ConnectAsync(CancellationToken token = default(CancellationToken))
    {
        CheckDisposed();
        token.ThrowIfCancellationRequested();

        if (_subscriber != null)
        {
            return;
        }

        await _connectionLock.WaitAsync(token).ConfigureAwait(false);
        try
        {
            if (_subscriber == null)
            {
                if (_options.ConnectionMultiplexerFactory is null)
                {
                    if (_options.ConfigurationOptions is not null)
                    {
                        _connection = await ConnectionMultiplexer.ConnectAsync(_options.ConfigurationOptions).ConfigureAwait(false);
                    }
                    else
                    {
                        _connection = await ConnectionMultiplexer.ConnectAsync(_options.Configuration).ConfigureAwait(false);
                    }
                }
                else
                {
                    _connection = await _options.ConnectionMultiplexerFactory();
                }

                _subscriber = _connection.GetSubscriber();
            }
        }
        finally
        {
            _connectionLock.Release();
        }
    }

    public virtual void StartSubscribe()
    {
        Connect();

        if (_started)
        {
            return;
        }

        _startLock.Wait();

        try
        {
            if (!_started)
            {
                foreach (var subscriber in _subscribeExecuter.Subscribers)
                {
                    var eventType = subscriber.Key;
                    var eventName = subscriber.Key.Name;

                    // 创建 Action<RedisChannel, RedisValue> 委托
                    var handler = CreateDelegateConsumerReceived(eventType);

                    // 订阅
                    Subscriber.Subscribe(eventName, handler);
                }

                _started = true;
            }
        }
        finally
        {
            _startLock.Release();
        }
    }

    public virtual async Task PublishAsync<TEvent>(TEvent @event, CancellationToken token = default)
        where TEvent : class, IEventModel
    {
        await ConnectAsync(token);

        var eventName = @event.GetType().Name;
        var message = JsonHelper.Serialize(@event);

        await Subscriber.PublishAsync(eventName, message);
    }

    protected virtual Action<RedisChannel, RedisValue> CreateDelegateConsumerReceived(Type eventType)
    {
        Func<object, Task> processEvent = _subscribeExecuter.ProcessEvent;

        Action<RedisChannel, RedisValue> handler = async (channel, message) =>
        {
            // 获取消息
            var @event = JsonHelper.Deserialize(message.ToString(), eventType);

            // 处理消息
            if (@event != null) await processEvent.Invoke(@event).ConfigureAwait(false);
        };

        return handler;
    }

    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }
        _disposed = true;
        _connection?.Dispose();
    }

    private void CheckDisposed()
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(this.GetType().FullName);
        }
    }
}
