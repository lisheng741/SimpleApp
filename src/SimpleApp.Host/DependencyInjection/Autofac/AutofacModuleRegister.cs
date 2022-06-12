using Autofac;

namespace SimpleApp.Host.DependencyInjection.Autofac;

public class AutofacModuleRegister : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        // 后续可以改成读取配置
        var dllNames = new List<string>()
        {
            "SimpleApp.Services"
        };

        var assemblies = AssemblyHelper.GetAssemblies(dllNames);

        foreach(var assembly in assemblies)
        {
            // 支持属性注入依赖重复
            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces().InstancePerDependency()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
        }
    }
}
