namespace Simple.Repository;

/// <summary>
/// 数据筛选器管理
/// </summary>
public class DataFilter
{
    /// <summary>
    /// 查询时：是否忽略软删筛选器。
    /// 删除时：是否忽略软删，是-硬删，否-软删。
    /// </summary>
    public bool IgnoreSoftDelete { get; set; } = false;

    /// <summary>
    /// 是否忽略多租户
    /// </summary>
    public bool IgnoreTenant { get; set; } = false;
}
