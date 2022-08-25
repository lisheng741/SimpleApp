namespace Simple.Repository;

/// <summary>
/// 数据筛选器管理
/// </summary>
public class DataFilter
{
    private static readonly DataFilter _default = new DataFilter(false, false);

    /// <summary>
    /// 查询时：是否忽略软删筛选器。
    /// 删除时：是否忽略软删。
    /// （是-硬删，否-软删）
    /// </summary>
    public bool IgnoreSoftDelete { get; set; }

    /// <summary>
    /// 是否忽略多租户。
    /// </summary>
    public bool IgnoreTenant { get; set; }

    static DataFilter()
    {
    }

    public DataFilter()
    {
        IgnoreSoftDelete = _default.IgnoreSoftDelete;
        IgnoreTenant = _default.IgnoreTenant;
    }

    protected DataFilter(bool ignoreSoftDele, bool ignoreTenant)
    {
        IgnoreSoftDelete = ignoreSoftDele;
        IgnoreTenant = ignoreTenant;
    }

    /// <summary>
    /// 重置为 false
    /// </summary>
    /// <returns></returns>
    public bool ResetIgnoreSoftDelete()
    {
        IgnoreSoftDelete = _default.IgnoreSoftDelete;
        return IgnoreSoftDelete;
    }

    /// <summary>
    /// 重置为 false
    /// </summary>
    /// <returns></returns>
    public bool ResetIgnoreTenant()
    {
        IgnoreTenant = _default.IgnoreTenant;
        return IgnoreTenant;
    }

    /// <summary>
    /// 重置所有数据过滤器为 false
    /// </summary>
    public void Reset()
    {
        ResetIgnoreSoftDelete();
        ResetIgnoreTenant();
    }
}
