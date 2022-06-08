using Autofac;
using Autofac.Extensions.DependencyInjection;
using SimpleApp.Host.DependencyInjection.Autofac;

namespace Microsoft.Extensions.Hosting;

public static class IHostBuilderExtensions
{
    public static IHostBuilder UseAutofacProviderFactory(this IHostBuilder builder)
    {
        builder.UseServiceProviderFactory(new AutofacServiceProviderFactory(containerBuilder =>
        {
            containerBuilder.RegisterModule<AutofacModuleRegister>();
        }));

        return builder;
    }
}
