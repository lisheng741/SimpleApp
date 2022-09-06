namespace Simple.Common.EventBus.Core;

/// <summary>
/// 订阅者字典列表
/// </summary>
public class SubscriberDictionary
{
    private readonly IDictionary<Type, List<Type>> _subscribers = new Dictionary<Type, List<Type>>();

    internal SubscriberDictionary()
    {
    }

    public void Add<TEvent, THandler>()
        where TEvent : class, IEventModel
        where THandler : IEventHandler<TEvent>
    {
        var eventType = typeof(TEvent);
        var handlerType = typeof(THandler);

        if (!_subscribers.ContainsKey(eventType))
        {
            _subscribers[eventType] = new List<Type>();
        }

        _subscribers[eventType].Add(handlerType);
    }

    internal void Add(SubscriberDictionary subscribers)
    {
        foreach (var subscriber in subscribers.ToDictionary())
        {
            _subscribers.Add(subscriber);
        }
    }

    internal IDictionary<Type, List<Type>> ToDictionary()
    {
        return _subscribers;
    }
}
