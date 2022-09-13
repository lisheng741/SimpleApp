using System.Reflection;
using AutoMapper;

namespace Simple.Repository.DataSeed;

public static class ModelBuilderExtensions
{
    /// <summary>
    /// 配置种子数据
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="assemblies"></param>
    public static void ConfigureDataSeed(this ModelBuilder builder,params Assembly[] assemblies)
    {
        List<Type> types = assemblies.SelectMany(a => a.GetTypes())
                                     .Where(t => !t.IsAbstract && t.IsAssignableTo(typeof(IConfigureDataSeed)))
                                     .ToList();

        foreach(var type in types)
        {
            // 创建模型实例，调用实例的 ConfigureDataSeed 进行专有配置
            var dataSeed = ActivatorHelper.CreateInstance(type) as IConfigureDataSeed;
            if(dataSeed != null)
            {
                dataSeed.ConfigureDataSeed(builder);
            }
        }
    }
}
