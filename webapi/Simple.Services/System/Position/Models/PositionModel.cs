namespace Simple.Services;

/// <summary>
/// 岗位
/// </summary>
public class PositionModel : ModelBase
{
    /// <summary>
    /// 主键
    /// </summary>
    public Guid? Id { get; set; }

    /// <summary>
    /// 编码
    /// </summary>
    [Required(ErrorMessage = "编码是必输的"), 
        MaxLength(128, ErrorMessage = "编码长度不能超过128个字符")]
    public string Code { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    [Required(ErrorMessage = "岗位名称是必输的"), 
        MaxLength(128, ErrorMessage = "名称长度不能超过128个字符")]
    public string Name { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    public int Sort { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [MaxLength(2048, ErrorMessage = "备注长度不能超过2048个字符")]
    public string? Remark { get; set; }

    /// <summary>
    /// 启用状态
    /// </summary>
    public bool IsEnabled { get; set; } = true;

    public PositionModel(string code, string name)
    {
        Code = code;
        Name = name;
    }

    public override void ConfigureMapper(Profile profile)
    {
        profile.CreateMap<SysPosition, PositionModel>();

        profile.CreateMap<PositionModel, SysPosition>()
            .ForMember(d => d.Id, options => options.Ignore());
    }
}
