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
        // 对象映射 AutoMapper
        var profileDllNames = new List<string>()
        {
            "SimpleApp.Services",
            "SimpleApp.Services.Contracts"
        };
        var assemblies = AssemblyHelper.GetAssemblies(profileDllNames);
        services.AddAutoMapper(assemblies);

        // 认证
        services.AddJwtAuthentication();
        // 授权
        services.AddPermission();

        return services;
    }
}
