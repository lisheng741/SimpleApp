using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Simple.Common.EFCore;

namespace Microsoft.Extensions.DependencyInjection;

public static class EFCoreServiceCollectionExtensions
{
    /// <summary>
    /// 添加 EF 配置
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddSimpleEFCore(this IServiceCollection services)
    {
        services.Configure<DbContextOptionsBuilder>(options =>
        {
            options.ConfigureWarnings(builder =>
            {
                // 消除 https://go.microsoft.com/fwlink/?linkid=2131316
                builder.Ignore(CoreEventId.PossibleIncorrectRequiredNavigationWithQueryFilterInteractionWarning);
            });
        });

        // 注入默认数据提供者
        services.AddScoped<IEntityDataProvider, DefaultEntityDataProvider>();

        return services;
    }
}
