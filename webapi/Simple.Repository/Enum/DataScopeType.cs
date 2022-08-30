namespace Simple.Repository.Enum;

public enum DataScopeType
{
    /// <summary>
    /// 全部数据
    /// </summary>
    All = 1,

    /// <summary>
    /// 本部门及以下数据
    /// </summary>
    DepartmentWithChildren = 2,

    /// <summary>
    /// 本部门数据
    /// </summary>
    Department = 3,

    /// <summary>
    /// 仅本人数据
    /// </summary>
    Personal = 4,

    /// <summary>
    /// 自定义数据
    /// </summary>
    Custom = 5
}
