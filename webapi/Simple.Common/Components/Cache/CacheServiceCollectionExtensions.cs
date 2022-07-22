namespace Microsoft.Extensions.DependencyInjection;

public static class CacheServiceCollectionExtensions
{
    /// <summary>
    /// 注册缓存服务，如有配置 Redis 则启用，没有则启用分布式内存缓存（DistributedMemoryCache）
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddCache(this IServiceCollection services)
    {
        // 根据情况，启用 Redis 或 DistributedMemoryCache
        if (AppSettings.RedisEnabled)
        {
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = AppSettings.RedisConnectionString;
                options.InstanceName = AppSettings.RedisInstance;
            });
        }
        else
        {
            services.AddDistributedMemoryCache();
        }

        return services;
    }
}
