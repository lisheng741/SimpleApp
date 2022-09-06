using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Simple.Common.EventBus.Core;

public class EventBusBuilder
{
    public IServiceCollection Services { get; private set; }

    /// <summary>
    /// 订阅者字典列表
    /// </summary>
    public SubscriberDictionary Subscribers { get; private set; }

    public EventBusBuilder(IServiceCollection services, SubscriberDictionary subscribers)
    {
        Services = services;
        Subscribers = subscribers;
    }

    /// <summary>
    /// 配置订阅者字典列表
    /// </summary>
    /// <param name="configAction">配置程序</param>
    /// <returns></returns>
    public EventBusBuilder AddSubscriber(Action<SubscriberDictionary> configAction)
    {
        var subscribers = new SubscriberDictionary();
        configAction(subscribers);

        // 添加到订阅者字典列表
        Subscribers.Add(subscribers);

        // 将所有订阅者注入为 Transient 服务
        foreach (var subscriber in subscribers.ToDictionary())
        {
            foreach (Type type in subscriber.Value)
            {
                //services.AddTransient(type);
                Services.Replace(new ServiceDescriptor(type, type, ServiceLifetime.Transient));
            }
        }

        return this;
    }
}
