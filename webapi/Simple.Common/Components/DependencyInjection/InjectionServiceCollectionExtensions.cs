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
    public static IServiceCollection AddAutoServices(this IServiceCollection services, params string[] dllNames)
    {
        // 根据名称获取程序集
        var assemblies = AssemblyHelper.GetAssemblies(dllNames);

        // 获取程序集内名称以 Service 结尾的 class
        var serviceTypes = assemblies.SelectMany(a => a.GetTypes())
                                     .Where(t => t.Name.EndsWith("Service") && t.IsClass && !t.IsAbstract)
                                     .Distinct();

        // 遍历，将服务默认注册为瞬态服务（生命周期：Transient）
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
