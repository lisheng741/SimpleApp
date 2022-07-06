using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection;

public static class EfCoreServiceCollectionExtensions
{
    /// <summary>
    /// 添加仓储设置
    /// </summary>
    /// <param name="services"></param>
    /// <param name="connectionString"></param>
    /// <returns></returns>
    public static IServiceCollection AddRepository(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<SimpleDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });

        return services;
    }
}
