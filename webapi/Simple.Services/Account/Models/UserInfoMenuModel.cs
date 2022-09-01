namespace Simple.Services;

public class UserInfoMenuModel : ModelBase
{
    /// <summary>
    /// 主键Id
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// 父节点Id
    /// </summary>
    public Guid Pid { get; set; }

    /// <summary>
    /// 应用分类
    /// </summary>
    public string? Application { get; set; }

    /// <summary>
    /// 路由名称, 必须设置,且不能重名
    /// </summary>
    public string Name { get; set; } = "";

    /// <summary>
    /// 组件
    /// </summary>
    public string? Component { get; set; }

    /// <summary>
    /// 重定向地址, 访问这个路由时, 自定进行重定向
    /// </summary>
    public string? Redirect { get; set; }

    /// <summary>
    /// 路由元信息（路由附带扩展信息）
    /// </summary>
    public UserInfoMenuMetaModel Meta { get; set; } = default!;

    /// <summary>
    /// 路径
    /// </summary>
    public string? Path { get; set; }

    /// <summary>
    /// 控制路由和子路由是否显示在 sidebar
    /// </summary>
    public bool Hidden { get; set; } = false;


    public override void ConfigureMapper(Profile profile)
    {
        profile.CreateMap<SysMenu, UserInfoMenuModel>()
            .ForMember(d => d.Pid, options => options.MapFrom(s => s.ParentId))
            .ForMember(d => d.Name, options => options.MapFrom(s => s.Code))
            .ForMember(d => d.Redirect, options => options.MapFrom(s => s.OpenType == MenuOpenType.Outer ? s.Link : s.Redirect))
            .ForMember(d => d.Path, options => options.MapFrom(s => s.OpenType == MenuOpenType.Outer ? s.Link : s.Router))
            .ForMember(d => d.Hidden, options => options.MapFrom(s => s.Visible == "N"))
            .ForMember(d => d.Meta, options => options.MapFrom(s => 
                new UserInfoMenuMetaModel()
                {
                    Title = s.Name,
                    Icon = s.Icon,
                    Show = s.Visible == "Y",
                    Link = s.Link,
                    Target = s.OpenType == MenuOpenType.Outer ? "_blank" : ""
                }
            ));

        profile.CreateMap<MenuCacheItem, UserInfoMenuModel>()
            .ForMember(d => d.Pid, options => options.MapFrom(s => s.ParentId))
            .ForMember(d => d.Name, options => options.MapFrom(s => s.Code))
            .ForMember(d => d.Redirect, options => options.MapFrom(s => s.OpenType == MenuOpenType.Outer ? s.Link : s.Redirect))
            .ForMember(d => d.Path, options => options.MapFrom(s => s.OpenType == MenuOpenType.Outer ? s.Link : s.Router))
            .ForMember(d => d.Hidden, options => options.MapFrom(s => s.Visible == "N"))
            .ForMember(d => d.Meta, options => options.MapFrom(s =>
                new UserInfoMenuMetaModel()
                {
                    Title = s.Name,
                    Icon = s.Icon,
                    Show = s.Visible == "Y",
                    Link = s.Link,
                    Target = s.OpenType == MenuOpenType.Outer ? "_blank" : ""
                }
            ));
    }
}

public class UserInfoMenuMetaModel
{
    /// <summary>
    /// 路由标题, 用于显示面包屑, 页面标题 *推荐设置
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// 图标
    /// </summary>
    public string? Icon { get; set; }

    /// <summary>
    /// 是否可见
    /// </summary>
    public bool? Show { get; set; }

    /// <summary>
    /// 如需外部打开，增加：_blank
    /// </summary>
    public string? Target { get; set; }

    /// <summary>
    /// 内链打开http链接
    /// </summary>
    public string? Link { get; set; }
}
