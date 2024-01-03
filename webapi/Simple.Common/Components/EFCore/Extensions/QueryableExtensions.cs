namespace System.Linq;

public static class QueryableExtensions
{
    /// <summary>
    /// 分页查询
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="source"></param>
    /// <param name="pageIndex">页码，从1开始</param>
    /// <param name="pageSize">每页记录数</param>
    /// <returns></returns>
    public static IQueryable<TSource> Page<TSource>(this IQueryable<TSource> source, int pageIndex = 1, int pageSize = 10)
    {
        return source.Skip((pageIndex - 1) * pageSize).Take(pageSize);
    }
}
