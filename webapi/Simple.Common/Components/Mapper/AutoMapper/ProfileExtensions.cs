namespace AutoMapper;

public static class ProfileExtensions
{
    /// <summary>
    /// 自动配置调用者所在的程序集所有实现了 IConfigureMapper 接口的配置
    /// </summary>
    /// <param name="profile"></param>
    public static void ConfigureMapper(this Profile profile)
    {
        // 获取当前程序集所有实现
        List<Type> types = profile.GetType().Assembly.GetTypes()
                                  .Where(t => !t.IsAbstract && t.IsAssignableTo(typeof(IConfigureMapper)))
                                  .ToList();

        foreach(var type in types)
        {
            // 创建模型实例，调用实例的 ConfigureMapper 进行专有配置
            var model = ActivatorHelper.CreateInstance(type) as IConfigureMapper;
            if(model != null)
            {
                model.ConfigureMapper(profile);
            }
        }
    }
}
