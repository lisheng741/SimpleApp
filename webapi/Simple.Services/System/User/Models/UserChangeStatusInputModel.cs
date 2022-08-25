namespace Simple.Services;

public class UserChangeStatusInputModel
{
    /// <summary>
    /// 主键Id
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// 启用状态（1-启用，0-禁用）
    /// </summary>
    public int Status { get; set; } = 1;
}
