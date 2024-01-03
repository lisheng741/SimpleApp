using System.Runtime.Loader;
using Microsoft.Extensions.DependencyModel;

namespace Simple.Common.Helpers;

public static class AssemblyHelper
{
    /// <summary>
    /// 获取项目程序集（排除系统程序集、NuGet包）
    /// </summary>
    /// <returns></returns>
    public static List<Assembly> GetAssemblies()
    {
        // 参考：https://www.cnblogs.com/yanglang/p/6866165.html

        var result = new List<Assembly>();
        var libs = DependencyContext.Default?.CompileLibraries.Where(lib => !lib.Serviceable && lib.Type != "package");
        if (libs == null)
        {
            throw new ArgumentNullException(nameof(libs));
        }

        foreach (var lib in libs)
        {
            var assembly = AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(lib.Name));
            result.Add(assembly);
        }
        return result;
    }

    /// <summary>
    /// 将程序集加载到 AssemblyLoadContext.Default 中，并且获取这个程序集。
    /// （这个方法，主要是为了解决程序集没有被加载的情况）
    /// </summary>
    /// <param name="dllNames">程序集的名称，如：Test 或 Test.dll</param>
    /// <returns></returns>
    /// <exception cref="DllNotFoundException"></exception>
    public static List<Assembly> GetAssemblies(params string[] dllNames)
    {
        var basePath = AppContext.BaseDirectory;

        var assemblies = new List<Assembly>();
        var dllFileFullNames = new List<string>();

        foreach (var dllName in dllNames)
        {
            var dllFileName = dllName.ToLower().EndsWith(".dll") ? dllName : $"{dllName}.dll";
            var dllFileFullName = Path.Combine(basePath, dllFileName);

            // 如果 DLL 不存在，抛出异常
            if (!File.Exists(dllFileFullName))
            {
                throw new DllNotFoundException(dllFileName);
            }

            dllFileFullNames.Add(dllFileFullName);
        }

        foreach (var dllFileFullName in dllFileFullNames)
        {
            var assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(dllFileFullName);
            assemblies.Add(assembly);
        }

        return assemblies;
    }
}
