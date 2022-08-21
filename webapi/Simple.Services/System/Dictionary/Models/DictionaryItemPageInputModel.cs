namespace Simple.Services;

public class DictionaryItemPageInputModel : PageInputModel
{
    /// <summary>
    /// 字典Id
    /// </summary>
    public Guid? TypeId { get; set; }

    /// <summary>
    /// 编码
    /// </summary>
    public override string? Code { get; set; }

    /// <summary>
    /// 字典值
    /// </summary>
    public string? Value { get; set; }
}
