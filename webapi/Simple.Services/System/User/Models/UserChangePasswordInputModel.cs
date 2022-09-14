namespace Simple.Services;

public class UserChangePasswordInputModel
{
    /// <summary>
    /// 主键Id
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// 原密码
    /// </summary>
    [Required(ErrorMessage = "原密码不能为空")]
    public string Password { get; set; } = "";

    /// <summary>
    /// 新密码
    /// </summary>
    [Required(ErrorMessage = "新密码不能为空")]
    public string NewPassword { get; set; } = "";
}
