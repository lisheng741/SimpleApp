namespace Simple.Services;

/// <summary>
/// 字典
/// </summary>
public class DictionaryModel : ModelBase
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
    /// 启用状态（1-启用，0-禁用）
    /// </summary>
    public int Status { get; set; } = 1;


    public override void ConfigureMapper(Profile profile)
    {
        profile.CreateMap<SysDictionary, DictionaryModel>()
            .ForMember(d => d.Status, options => options.MapFrom(s => s.IsEnabled ? 1 : 0));

        profile.CreateMap<DictionaryModel, SysDictionary>()
            .ForMember(d => d.Id, options => options.Ignore())
            .ForMember(d => d.IsEnabled, options => options.MapFrom(s => s.Status == 1));
    }
}
