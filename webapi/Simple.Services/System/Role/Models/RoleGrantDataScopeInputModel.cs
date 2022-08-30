namespace Simple.Services;

public class RoleGrantDataScopeInputModel
{
    /// <summary>
    /// 角色Id
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// 数据范围（1-全部数据，2-本部门及以下数据，3-本部门数据，4-仅本人数据，5-自定义数据）
    /// </summary>
    public DataScopeType DataScopeType { get; set; } = DataScopeType.All;

    /// <summary>
    /// 组织Id列表
    /// </summary>
    public Guid[] GrantOrgIdList { get; set; } = new Guid[0];
}
