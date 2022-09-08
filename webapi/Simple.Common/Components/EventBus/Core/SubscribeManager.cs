using Microsoft.Extensions.Logging;

namespace Simple.Common.EventBus.Core;

public class SubscribeManager : ISubscribeManager
{
    private readonly ILogger<SubscribeManager> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly SubscriberDictionary _subscriberDictionary;

    public IDictionary<Type, List<Type>> Subscribers => _subscriberDictionary.ToDictionary();

    public SubscribeManager(ILogger<SubscribeManager> logger, 
                            IServiceProvider serviceProvider, 
                            SubscriberDictionary subscriberDictionary)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _subscriberDictionary = subscriberDictionary;
    }

    public virtual async Task ProcessEvent(object @event)
    {
        Type eventType = @event.GetType();
        if (Subscribers.TryGetValue(eventType, out var handlerTypes))
        {
            using var scope = _serviceProvider.CreateScope();
            foreach (var handlerType in handlerTypes)
            {
                try
                {
                    var handler = scope.ServiceProvider.GetService(handlerType);
                    var handle = handlerType.GetMethod("Handle");

                    //var handlerGenericType = typeof(IEventHandler<>).MakeGenericType(eventType);
                    //var handle = handlerGenericType.GetMethod("Handle");

                    if (handle != null)
                    {
                        // 调用 Handle 方法
                        var result = handle.Invoke(handler, new object[] { @event });

                        // 如果返回 Task 则等待
                        if (result is Task task)
                        {
                            await task;
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError($"事件 {eventType.Name} 发生错误，错误信息：{ex.Message}");
                }
            }
        }
    }
}
