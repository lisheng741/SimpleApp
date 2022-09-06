namespace Simple.Common.EventBus.Redis;

public class RedisEventPublisher : IEventPublisher
{
    private readonly IRedisManager _redisManager;

    public RedisEventPublisher(IRedisManager redisManager)
    {
        _redisManager = redisManager;
    }

    public Task PublishAsync<TEvent>(TEvent @event)
        where TEvent : class, IEventModel
    {
        return _redisManager.PublishAsync(@event);
    }
}
