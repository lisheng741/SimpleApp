using Microsoft.AspNetCore.Cors.Infrastructure;

namespace Microsoft.Extensions.DependencyInjection;

public static class CorsServiceCollectionExtensions
{
    /// <summary>
    /// 添加默认跨域配置
    /// </summary>
    /// <param name="services"></param>
    /// <param name="setupAction"></param>
    /// <returns></returns>
    public static IServiceCollection AddSimpleCors(this IServiceCollection services, Action<CorsOptions>? setupAction =null)
    {
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                if(AppSettings.AllowCors.Any(c => c == "*"))
                {
                    // 允许任意跨域
                    policy.AllowAnyOrigin();
                }
                else
                {
                    // 允许指定域名
                    policy.WithOrigins(AppSettings.AllowCors); 
                }
            });
        });

        // 自定义配置
        if(setupAction != null)
        {
            services.Configure<CorsOptions>(setupAction);
        }

        return services;
    }
}
