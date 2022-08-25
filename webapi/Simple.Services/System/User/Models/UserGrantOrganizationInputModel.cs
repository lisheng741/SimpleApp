namespace Simple.Services;

public class UserGrantOrganizationInputModel
{
    /// <summary>
    /// 用户Id
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// 拥有的组织Id列表
    /// </summary>
    public List<Guid> GrantOrgIdList { get; set; } = new List<Guid>();
}
