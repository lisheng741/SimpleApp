namespace Simple.Services;

/// <summary>
/// 应用
/// </summary>
public class ApplicationModel : ModelBase
{
    /// <summary>
    /// 主键
    /// </summary>
    public Guid? Id { get; set; }

    /// <summary>
    /// 编码
    /// </summary>
    [MaxLength(128)]
    public string Code { get; set; } = "";

    /// <summary>
    /// 名称
    /// </summary>
    [MaxLength(128)]
    public string Name { get; set; } = "";

    /// <summary>
    /// 排序
    /// </summary>
    public int Sort { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [MaxLength(2048)]
    public string? Remark { get; set; }

    /// <summary>
    /// 启用状态: 1-启用，0-禁用
    /// </summary>
    public int Status { get; set; } = 1;

    /// <summary>
    /// 是否激活（只能同时激活一个应用）。
    /// 表示用户登录以后默认展示的应用。
    /// 值为 Y 或 N
    /// </summary>
    public string Active { get; set; } = "Y";


    public override void ConfigureMapper(Profile profile)
    {
        profile.CreateMap<SysApplication, ApplicationModel>()
            .ForMember(d => d.Status, options => options.MapFrom(s => s.IsEnabled ? 1 : 0))
            .ForMember(d => d.Active, options => options.MapFrom(s => s.IsActive ? "Y" : "N"));

        profile.CreateMap<ApplicationModel, SysApplication>()
            .ForMember(d => d.Id, options => options.Ignore())
            .ForMember(d => d.IsEnabled, options => options.MapFrom(s => s.Status == 1))
            .ForMember(d => d.IsActive, options => options.MapFrom(s => s.Active == "Y"));
    }
}
