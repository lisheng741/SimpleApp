namespace Microsoft.Extensions.DependencyInjection;

public static class CacheServiceCollectionExtensions
{
    /// <summary>
    /// 注册缓存服务，如有配置 Redis 则启用，没有则启用分布式内存缓存（DistributedMemoryCache）
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddSimpleCache(this IServiceCollection services)
    {
        // 根据情况，启用 Redis 或 DistributedMemoryCache
        if (AppSettings.Redis.Enabled)
        {
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = AppSettings.Redis.ConnectionString;
                options.InstanceName = AppSettings.Redis.Instance;
            });
        }
        else
        {
            services.AddDistributedMemoryCache();
        }

        return services;
    }
}
