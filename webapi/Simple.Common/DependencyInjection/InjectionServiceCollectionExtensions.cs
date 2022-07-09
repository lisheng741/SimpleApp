using Simple.Common.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection;

public static class InjectionServiceCollectionExtensions
{
    /// <summary>
    /// 自动注册程序集内以 Service 结尾的服务
    /// </summary>
    /// <param name="services"></param>
    /// <param name="dllNames"></param>
    /// <returns></returns>
    public static IServiceCollection AddServices(this IServiceCollection services, params string[] dllNames)
    {
        var assemblies = AssemblyHelper.GetAssemblies(dllNames);

        var serviceTypes = assemblies.SelectMany(a => a.GetTypes())
                                     .Where(t => t.Name.EndsWith("Service") && t.IsClass && !t.IsAbstract)
                                     .Distinct();

        foreach (var serviceType in serviceTypes)
        {
            var lifecyleType = LifecycleType.Transient;

            var autoInjection = serviceType.GetCustomAttribute<AutoInjectionAttribute>();

            if (autoInjection != null)
            {
                if (!autoInjection.AutoRegister)
                {
                    continue;
                }
                lifecyleType = autoInjection.Lifecycle;
            }

            switch (lifecyleType)
            {
                case LifecycleType.Singleton:
                    services.AddSingleton(serviceType);
                    break;
                case LifecycleType.Scoped:
                    services.AddScoped(serviceType);
                    break;
                case LifecycleType.Transient:
                default:
                    services.AddTransient(serviceType);
                    break;
            }
        }

        return services;
    }
}
