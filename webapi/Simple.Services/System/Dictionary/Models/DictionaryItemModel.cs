namespace Simple.Services;

/// <summary>
/// 字典子项
/// </summary>
public class DictionaryItemModel : ModelBase
{
    /// <summary>
    /// 主键
    /// </summary>
    public Guid? Id { get; set; }

    /// <summary>
    /// 字典Id. DictionaryId
    /// </summary>
    public Guid TypeId { get; set; }

    /// <summary>
    /// 编码
    /// </summary>
    [MaxLength(128)]
    public string Code { get; set; } = "";

    /// <summary>
    /// 字典值
    /// </summary>
    [MaxLength(128)]
    public string Value { get; set; } = "";

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


    public override void ConfigureMapper(Profile profile)
    {
        profile.CreateMap<SysDictionaryItem, DictionaryItemModel>()
            .ForMember(d => d.Value, options => options.MapFrom(s => s.Name))
            .ForMember(d => d.TypeId, options => options.MapFrom(s => s.DictionaryId))
            .ForMember(d => d.Status, options => options.MapFrom(s => s.IsEnabled ? 1 : 0));

        profile.CreateMap<DictionaryItemModel, SysDictionaryItem>()
            .ForMember(d => d.Id, options => options.Ignore())
            .ForMember(d => d.Name, options => options.MapFrom(s => s.Value))
            .ForMember(d => d.DictionaryId, options => options.MapFrom(s => s.TypeId))
            .ForMember(d => d.IsEnabled, options => options.MapFrom(s => s.Status == 1));
    }
}
