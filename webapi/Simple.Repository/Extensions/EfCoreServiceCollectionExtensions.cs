using System.Reflection;
using Microsoft.EntityFrameworkCore.Diagnostics;

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
            options.ConfigureWarnings(builder =>
            {
                // 消除 https://go.microsoft.com/fwlink/?linkid=2131316
                builder.Ignore(CoreEventId.PossibleIncorrectRequiredNavigationWithQueryFilterInteractionWarning);
            });
        });

        return services;
    }
}
