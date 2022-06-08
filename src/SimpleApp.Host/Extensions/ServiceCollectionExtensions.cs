namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSingletonServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton(new AppSettingsConfig(configuration));

        return services;
    }
}
