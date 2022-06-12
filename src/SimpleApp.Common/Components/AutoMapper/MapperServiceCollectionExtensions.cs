using System.Reflection;
using AutoMapper;

namespace Microsoft.Extensions.DependencyInjection;

public static class MapperServiceCollectionExtensions
{
    /// <summary>
    /// 不建议使用
    /// </summary>
    /// <param name="services"></param>
    /// <param name="assemblies"></param>
    /// <returns></returns>
    [Obsolete("不建议使用")]
    public static IServiceCollection AddObjectMapper(this IServiceCollection services, IEnumerable<Assembly> assemblies)
    {
        //services.TryAdd(new ServiceDescriptor(typeof(IObjectMapper), typeof(ObjectAutoMapper), ServiceLifetime.Scoped));

        var profileAssemblies = assemblies.Where(a =>
            a != typeof(Mapper).Assembly
            && a.GetTypes().Where(t => t.IsSubclassOf(typeof(Profile))).Any()
        ).Distinct();

        services.AddAutoMapper(profileAssemblies);

        return services;
    }
}
