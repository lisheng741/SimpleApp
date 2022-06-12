using System.Reflection;
using System.Runtime.Loader;

namespace SimpleApp.Common.Helpers;

public static class AssemblyHelper
{
    /// <summary>
    /// Load the assemblies into AssemblyLoadContext.Default, and get it
    /// </summary>
    /// <param name="dllNames"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static IEnumerable<Assembly> GetAssemblies(IEnumerable<string> dllNames)
    {
        var basePath = AppContext.BaseDirectory;

        var assemblies = new List<Assembly>();
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
            var assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(dllFileFullName);
            assemblies.Add(assembly);
        }

        return assemblies;
    }
}
