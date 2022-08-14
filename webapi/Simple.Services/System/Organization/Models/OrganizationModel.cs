namespace Simple.Services;

/// <summary>
/// 组织
/// </summary>
public class OrganizationModel
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
    [Required(ErrorMessage = "编码是必输的")]
    public string Code { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    [Required(ErrorMessage = "机构名称是必输的")]
    public string Name { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    public int Sort { get; set; }

    /// <summary>
    /// 启用状态
    /// </summary>
    public bool IsEnabled { get; set; } = true;

    public OrganizationModel(string code, string name)
    {
        Code = code;
        Name = name;
    }
}
