namespace Simple.Services;

public class DeleteInputModel
{
    public Guid Id { get; set; }
}

public class PageInputModel
{
    public virtual string Sort { get; set; } = "Sort";

    public virtual int PageNo { get; set; } = 1;

    public virtual int PageSize { get; set; } = 10;
}
