namespace Simple.Services;

public class IdInputModel
{
    public Guid Id { get; set; }
}

public class PageInputModel
{
    /// <summary>
    /// 排序字段，暂时无效（!Problem）
    /// </summary>
    public virtual string Sort { get; set; } = "Sort";

    /// <summary>
    /// 页码，从1开始
    /// </summary>
    public virtual int PageNo { get; set; } = 1;

    /// <summary>
    /// 每页记录数
    /// </summary>
    public virtual int PageSize { get; set; } = 10;

    /// <summary>
    /// 编码
    /// </summary>
    public virtual string? Code { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    public virtual string? Name { get; set; }
}
