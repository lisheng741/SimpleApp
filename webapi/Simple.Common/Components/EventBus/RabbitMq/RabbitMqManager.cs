using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Simple.Common.EventBus.RabbitMq;

public class RabbitMqManager : IRabbitMqManager, IDisposable
{
    private readonly ILogger<RabbitMqManager> _logger;
    private readonly IConnectionFactory _connectionFactory;
    private readonly ISubscribeManager _subscribeExecuter;
    private readonly RabbitMqEventBusOptions _options;

    private IConnection? _connection;
    private IModel? _consumerChannel;
    private bool _disposed;
    private bool _started;
    private readonly SemaphoreSlim _connectionLock = new SemaphoreSlim(1, 1);
    private readonly SemaphoreSlim _startLock = new SemaphoreSlim(1, 1);

    /// <summary>
    /// RabbitMQ 连接实例
    /// </summary>
    public IConnection Connection => _connection ?? throw new NullReferenceException(nameof(Connection));

    /// <summary>
    /// 消费者 Channel 实例
    /// </summary>
    public IModel ConsumerChannel => _consumerChannel ?? throw new NullReferenceException(nameof(ConsumerChannel));

    public string ExChangeName => _options.ExChangeName;

    public string ExChangeType => _options.ExChangeType;

    public string QueueName => _options.QueueName;

    public RabbitMqManager(IConnectionFactory connectionFactory,
                           ILogger<RabbitMqManager> logger,
                           ISubscribeManager subscribeExecuter,
                           IOptions<RabbitMqEventBusOptions> options)
    {
        _connectionFactory = connectionFactory;
        _logger = logger;
        _subscribeExecuter = subscribeExecuter;
        _options = options.Value;
    }

    private void Connect()
    {
        CheckDisposed();
        if (_connection != null)
        {
            return;
        }

        _connectionLock.Wait();
        try
        {
            if (_connection == null)
            {
                // 创建连接
                _connection = _connectionFactory.CreateConnection();

                // 创建 消费者 channel
                _consumerChannel = _connection.CreateModel();

                // 声明交换机和队列
                _consumerChannel.ExchangeDeclare(exchange: ExChangeName, type: ExChangeType);
                _consumerChannel.QueueDeclare(queue: QueueName, true, false, false, null);
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

                    // 绑定交换机，队列，路由
                    ConsumerChannel.QueueBind(queue: QueueName, exchange: ExChangeName, routingKey: eventName);

                    // 消费者
                    EventingBasicConsumer consumer = new EventingBasicConsumer(ConsumerChannel);

                    // 添加接收事件
                    consumer.Received += CreateDelegateConsumerReceived(eventType);

                    // 启动消费者
                    ConsumerChannel.BasicConsume(queue: QueueName, autoAck: false, consumer);
                }

                _started = true;
            }
        }
        finally
        {
            _startLock.Release();
        }
    }

    public virtual Task PublishAsync<TEvent>(TEvent @event)
        where TEvent : class, IEventModel
    {
        Connect();

        var eventName = @event.GetType().Name;

        using var channel = Connection.CreateModel();

        var message = JsonHelper.Serialize(@event);
        var body = Encoding.UTF8.GetBytes(message);

        var properties = channel.CreateBasicProperties();
        properties.DeliveryMode = 2; // persistent

        channel.BasicPublish(
            exchange: ExChangeName,
            routingKey: eventName,
            mandatory: true,
            basicProperties: properties,
            body: body);

        return Task.CompletedTask;
    }

    protected virtual EventHandler<BasicDeliverEventArgs> CreateDelegateConsumerReceived(Type eventType)
    {
        Func<object, Task> processEvent = _subscribeExecuter.ProcessEvent;

        EventHandler<BasicDeliverEventArgs> eventHandler = async (sender, e) =>
        {
            // 获取消息
            //var eventName = e.RoutingKey;
            var message = Encoding.UTF8.GetString(e.Body.Span);
            var @event = JsonHelper.Deserialize(message, eventType);

            // 处理消息
            if (@event != null) await processEvent.Invoke(@event);

            // 标记消费
            ConsumerChannel.BasicAck(e.DeliveryTag, multiple: false);
        };

        return eventHandler;
    }

    public virtual void Dispose()
    {
        if (_disposed)
        {
            return;
        }
        _disposed = true;
        _consumerChannel?.Dispose();
        _connection?.Close();
    }

    private void CheckDisposed()
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(this.GetType().FullName);
        }
    }
}
