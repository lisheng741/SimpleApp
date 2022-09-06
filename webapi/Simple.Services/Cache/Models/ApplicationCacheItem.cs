namespace Simple.Services;

public class ApplicationCacheItem : ModelBase
{
    /// <summary>
    /// 编码
    /// </summary>
    public string Code { get; set; } = "";

    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; } = "";

    /// <summary>
    /// 是否激活（只能同时激活一个应用）。
    /// 表示用户登录以后默认展示的应用。
    /// </summary>
    public bool Active { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    public int Sort { get; set; }


    public override void ConfigureMapper(Profile profile)
    {
        profile.CreateMap<SysApplication, ApplicationCacheItem>()
            .ForMember(d => d.Active, options => options.MapFrom(s => s.IsActive));
    }
}
