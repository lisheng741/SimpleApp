namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSingletonServices(this IServiceCollection services, IConfiguration configuration)
    {
        AppSettings.Configuration = configuration;

        return services;
    }

    public static IServiceCollection AddCommonServices(this IServiceCollection services)
    {
        // 认证
        services.AddJwtAuthentication();

        return services;
    }
}
