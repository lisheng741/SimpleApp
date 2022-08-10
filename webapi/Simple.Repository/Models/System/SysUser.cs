namespace Simple.Repository.Models.System;

/// <summary>
/// 用户表
/// </summary>
[Index(nameof(UserName), IsUnique = true)]
public class SysUser : BusinessEntityBase<Guid>
{
    /// <summary>
    /// 用户名
    /// </summary>
    [MaxLength(64)]
    public string UserName { get; set; }

    /// <summary>
    /// 密码
    /// </summary>
    [MaxLength(64)]
    public string Password { get; set; }

    /// <summary>
    /// 姓名
    /// </summary>
    [MaxLength(32)]
    public string? Name { get; set; }

    /// <summary>
    /// 性别：1-男，2-女
    /// </summary>
    public GenderType Gender { get; set; } = GenderType.Unknown;

    /// <summary>
    /// 用户角色
    /// </summary>
    public List<SysUserRole> UserRoles { get; set; } = new List<SysUserRole>();
}
