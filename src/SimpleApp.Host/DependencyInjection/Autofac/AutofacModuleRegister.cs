using System.Runtime.Loader;
using Autofac;

namespace SimpleApp.Host.DependencyInjection.Autofac;

public class AutofacModuleRegister : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        var basePath = AppContext.BaseDirectory;
        //var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        //var assemblies = AssemblyLoadContext.Default.Assemblies;

        // 后续可以改成读取配置
        var dllNames = new List<string>()
        {
            "SimpleApp.Services"
        };
        var dllFileFullNames = new List<string>();

        foreach (var dllName in dllNames)
        {
            var dllFileName = dllName.EndsWith(".dll") ? dllName : dllName + ".dll";
            var dllFileFullName = Path.Combine(basePath, dllFileName);
            if (!File.Exists(dllFileFullName))
            {
                throw new Exception($"{dllFileName}不存在！");
            }
            dllFileFullNames.Add(dllFileFullName);
        }

        foreach (var dllFileFullName in dllFileFullNames)
        {
            // 获取程序集
            var assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(dllFileFullName);
            // 支持属性注入依赖重复
            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces().InstancePerDependency()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
        }
    }
}
