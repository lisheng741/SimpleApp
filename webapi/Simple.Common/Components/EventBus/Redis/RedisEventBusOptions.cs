using StackExchange.Redis;

namespace Simple.Common.EventBus.Redis;

public class RedisEventBusOptions
{
    /// <summary>
    /// 用于连接 Redis 的配置。
    /// </summary>
    public string Configuration { get; set; } = "127.0.0.1:6379";

    /// <summary>
    /// 用于连接 Redis 的配置。
    /// 优先级高于 Configuration .
    /// </summary>
    public ConfigurationOptions? ConfigurationOptions { get; set; }

    /// <summary>
    /// 获取或设置一个创建 ConnectionMultiplexer 实例的委托。
    /// </summary>
    public Func<Task<IConnectionMultiplexer>>? ConnectionMultiplexerFactory { get; set; }
}
