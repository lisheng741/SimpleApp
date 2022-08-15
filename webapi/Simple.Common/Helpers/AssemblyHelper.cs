using System.Runtime.Loader;

namespace Simple.Common.Helpers;

public static class AssemblyHelper
{
    /// <summary>
    /// 将程序集加载到 AssemblyLoadContext.Default 中，并且获取这个程序集。
    /// （这个方法，主要是为了解决程序集没有被加载的情况）.
    /// Load the assemblies into AssemblyLoadContext.Default, and get it.
    /// </summary>
    /// <param name="dllNames">程序集的名称，如：Test 或 Test.dll </param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static IEnumerable<Assembly> GetAssemblies(params string[] dllNames)
    {
        var basePath = AppContext.BaseDirectory;

        var assemblies = new List<Assembly>();
        var dllFileFullNames = new List<string>();

        foreach (var dllName in dllNames)
        {
            var dllFileName = dllName.ToLower().EndsWith(".dll") ? dllName : dllName + ".dll";
            var dllFileFullName = Path.Combine(basePath, dllFileName);

            if (!File.Exists(dllFileFullName))
            {
                throw new Exception($"{dllFileName}不存在！");
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

    /// <summary>
    /// 将程序集加载到 AssemblyLoadContext.Default 中，并且获取这个程序集。
    /// （这个方法，主要是为了解决程序集没有被加载的情况）.
    /// Load the assemblies into AssemblyLoadContext.Default, and get it.
    /// </summary>
    /// <param name="dllNames">程序集的名称，如：Test 或 Test.dll </param>
    /// <returns></returns>
    public static IEnumerable<Assembly> GetAssemblies(IEnumerable<string> dllNames)
        => GetAssemblies(dllNames.ToArray());
}
