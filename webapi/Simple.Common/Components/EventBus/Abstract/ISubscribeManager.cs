namespace Simple.Common.EventBus;

/// <summary>
/// 订阅管理器。
/// 主要负责提供订阅者列表，事件处理程序。
/// </summary>
public interface ISubscribeManager
{
    /// <summary>
    /// 订阅者列表
    /// </summary>
    IDictionary<Type, List<Type>> Subscribers { get; }

    /// <summary>
    /// 处理事件
    /// </summary>
    /// <param name="event"></param>
    /// <returns></returns>
    Task ProcessEvent(object @event);
}
