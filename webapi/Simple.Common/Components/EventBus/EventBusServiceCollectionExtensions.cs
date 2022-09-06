using Simple.Common.EventBus;
using Simple.Common.EventBus.Core;
using Simple.Common.EventBus.Local;
using Simple.Common.EventBus.RabbitMq;
using Simple.Common.EventBus.Redis;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace Microsoft.Extensions.DependencyInjection;

public static partial class EventBusServiceCollectionExtensions
{
    private static EventBusBuilder AddEventBusCore(this IServiceCollection services)
    {
        // 构建 EventBusBuilder
        SubscriberDictionary subscribers = new SubscriberDictionary();
        EventBusBuilder builder = new EventBusBuilder(services, subscribers);

        // 将 SubscriberDictionary 注为单例
        services.AddSingleton<SubscriberDictionary>(subscribers);

        // 注入基本服务
        services.AddSingleton<ISubscribeManager, SubscribeManager>();

        return builder;
    }

    public static EventBusBuilder AddEventBusLocal(this IServiceCollection services, Action<LocalEventBusOptions>? setupAction = null)
    {
        services.AddHostedService<LocalEventBusHostedService>();
        services.AddSingleton<IEventStore, ChannelEventStore>();
        services.AddSingleton<IEventPublisher, LocalEventPublisher>();

        // 自定义 EventBusOptions 配置
        if (setupAction != null) services.Configure(setupAction);

        return AddEventBusCore(services);
    }

    public static EventBusBuilder AddEventBusRabbitMq(this IServiceCollection services, Action<RabbitMqEventBusOptions>? setupAction = null)
    {
        // 注入默认连接工厂
        services.AddSingleton<IConnectionFactory>(sp =>
        {
            var options = sp.GetService<IOptions<RabbitMqEventBusOptions>>()?.Value;
            var connectionFactory = new ConnectionFactory();

            if (!string.IsNullOrEmpty(options?.HostName)) connectionFactory.HostName = options?.HostName;
            if (!string.IsNullOrEmpty(options?.UserName)) connectionFactory.UserName = options?.UserName;
            if (!string.IsNullOrEmpty(options?.Password)) connectionFactory.Password = options?.Password;
            if (!string.IsNullOrEmpty(options?.VirtualHost)) connectionFactory.Password = options?.VirtualHost;

            return connectionFactory;
        });

        // 基本服务
        services.AddHostedService<RabbitMqEventBusHostedService>();
        services.AddSingleton<IRabbitMqManager, RabbitMqManager>();
        services.AddSingleton<IEventPublisher, RabbitMqEventPublisher>();

        // 自定义配置
        if (setupAction != null) services.Configure(setupAction);

        return AddEventBusCore(services);
    }

    public static EventBusBuilder AddEventBusRedis(this IServiceCollection services,Action<RedisEventBusOptions>? setupAction = null)
    {
        // 基本服务
        services.AddHostedService<RedisEventBusHostedService>();
        services.AddSingleton<IRedisManager, RedisManager>();
        services.AddSingleton<IEventPublisher, RedisEventPublisher>();

        // 自定义配置
        if (setupAction != null) services.Configure(setupAction);

        return AddEventBusCore(services);
    }
}
