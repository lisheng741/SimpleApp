namespace Simple.Common.EventBus;

public interface IEventPublisher
{
    Task PublishAsync<TEvent>(TEvent @event)
        where TEvent : class, IEventModel;
}
