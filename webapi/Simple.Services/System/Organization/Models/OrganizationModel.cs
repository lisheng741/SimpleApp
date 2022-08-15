namespace Simple.Services;

/// <summary>
/// 组织
/// </summary>
public class OrganizationModel : ModelBase
{
    /// <summary>
    /// 主键
    /// </summary>
    public Guid? Id { get; set; }

    /// <summary>
    /// 父级Id
    /// </summary>
    public Guid Pid { get; set; }

    /// <summary>
    /// 编码
    /// </summary>
    [Required(ErrorMessage = "编码是必输的"), 
        MaxLength(128, ErrorMessage = "编码长度不能超过128个字符")]
    public string Code { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    [Required(ErrorMessage = "机构名称是必输的"),
        MaxLength(128, ErrorMessage = "名称长度不能超过128个字符")]
    public string Name { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    public int Sort { get; set; }

    /// <summary>
    /// 启用状态
    /// </summary>
    public bool IsEnabled { get; set; } = true;

    /// <summary>
    /// 备注
    /// </summary>
    [MaxLength(2048, ErrorMessage = "备注长度不能超过2048个字符")] 
    public string? Remark { get; set; }

    public OrganizationModel(string code, string name)
    {
        Code = code;
        Name = name;
    }

    public override void ConfigureMapper(Profile profile)
    {
        profile.CreateMap<SysOrganization, OrganizationModel>()
            .ForMember(d => d.Pid, options => options.MapFrom(s => s.ParentId));

        profile.CreateMap<OrganizationModel, SysOrganization>()
            .ForMember(d => d.Id, options => options.Ignore())
            .ForMember(d => d.ParentId, options => options.MapFrom(s => s.Pid));
    }
}
