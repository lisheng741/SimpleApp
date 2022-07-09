namespace Simple.Services.Account;

/// <summary>
/// 登录信息
/// </summary>
public class LoginInput
{
    /// <summary>
    /// 用户名
    /// </summary>
    [Required(ErrorMessage = "用户名不能为空！")]
    public string? Username { get; set; }

    /// <summary>
    /// 密码
    /// </summary>
    [Required(ErrorMessage = "密码不能为空！")]
    public string? Password { get; set; }
}
