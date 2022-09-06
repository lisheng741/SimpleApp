using RabbitMQ.Client;

namespace Simple.Common.EventBus.RabbitMq;

public interface IRabbitMqManager
{
    /// <summary>
    /// RabbitMQ 连接实例
    /// </summary>
    IConnection Connection { get; }

    /// <summary>
    /// 启用的交换机名称
    /// </summary>
    public string ExChangeName{ get; }

    /// <summary>
    /// 启用的交换机类型
    /// </summary>
    public string ExChangeType { get; }

    /// <summary>
    /// 启用的队列名称
    /// </summary>
    public string QueueName { get; }

    /// <summary>
    /// 启动订阅。
    /// </summary>
    void StartSubscribe();

    /// <summary>
    /// 发布消息。
    /// </summary>
    /// <typeparam name="TEvent"></typeparam>
    /// <param name="event"></param>
    /// <returns></returns>
    Task PublishAsync<TEvent>(TEvent @event)
        where TEvent : class, IEventModel;
}
