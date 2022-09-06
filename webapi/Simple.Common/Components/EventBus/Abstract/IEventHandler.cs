namespace Simple.Common.EventBus;

/// <summary>
/// 事件处理程序。
/// 即消费者（Subscriber）。
/// </summary>
/// <typeparam name="TEvent"></typeparam>
public interface IEventHandler<TEvent>
    where TEvent : class, IEventModel
{
    /// <summary>
    /// 处理程序
    /// </summary>
    /// <param name="event">事件模型</param>
    /// <returns></returns>
    Task Handle(TEvent @event);
}
