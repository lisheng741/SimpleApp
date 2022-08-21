namespace Simple.Services;

public class PageResultModel<T>
{
    public virtual int PageNo { get; set; }
    public virtual int PageSize { get; set; }
    public virtual int TotalPage { get; set; }
    public virtual int TotalRows { get; set; }
    public virtual List<T> Rows { get; set; } = new List<T>();

    /// <summary>
    /// 设置 PageNo 和 PageSize
    /// </summary>
    /// <param name="input"></param>
    public void SetPage(PageInputModel input)
    {
        PageNo = input.PageNo;
        PageSize = input.PageSize;
    }

    /// <summary>
    /// 根据 TotalRows 和 PageSize 计算出 TotalPage
    /// </summary>
    public int CountTotalPage()
    {
        if(PageSize == 0)
        {
            TotalPage = 0;
            return -1;
        }
        TotalPage = (int)Math.Ceiling(TotalRows / (double)PageSize);
        return TotalPage;
    }
}
