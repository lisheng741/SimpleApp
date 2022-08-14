namespace Simple.Services;

/// <summary>
/// 登录信息
/// </summary>
public class LoginModel
{
    /// <summary>
    /// 账号
    /// </summary>
    [Required(ErrorMessage = "账号不能为空！")]
    public string Account { get; set; }

    /// <summary>
    /// 密码
    /// </summary>
    [Required(ErrorMessage = "密码不能为空！")]
    public string Password { get; set; }

    public LoginModel(string account, string password)
    {
        Account = account;
        Password = password;
    }
}
