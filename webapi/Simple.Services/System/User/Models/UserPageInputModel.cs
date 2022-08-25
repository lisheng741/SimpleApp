namespace Simple.Services;

public class UserPageInputModel : PageInputModel
{
    /// <summary>
    /// 组织Id
    /// </summary>
    public Guid? OrganizationId { get; set; }

    /// <summary>
    /// 用户名/姓名模糊搜索
    /// </summary>
    public string? SearchValue { get; set; }

    /// <summary>
    /// 启用状态（1-启用，0-禁用）
    /// </summary>
    public int? SearchStatus { get; set; }
}
