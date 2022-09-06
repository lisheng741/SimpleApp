namespace Simple.Common.EventBus.RabbitMq;

public class RabbitMqEventPublisher : IEventPublisher
{
    private readonly IRabbitMqManager _rabbitMqManager;

    public RabbitMqEventPublisher(IRabbitMqManager rabbitMqManager)
    {
        _rabbitMqManager = rabbitMqManager;
    }

    public Task PublishAsync<TEvent>(TEvent @event)
        where TEvent : class, IEventModel
    {
        return _rabbitMqManager.PublishAsync(@event);
    }
}
