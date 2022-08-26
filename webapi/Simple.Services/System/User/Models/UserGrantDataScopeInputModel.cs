namespace Simple.Services;

public class UserGrantDataScopeInputModel
{
    /// <summary>
    /// 用户Id
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// 组织Id列表
    /// </summary>
    public Guid[] GrantOrgIdList { get; set; } = new Guid[0];
}
