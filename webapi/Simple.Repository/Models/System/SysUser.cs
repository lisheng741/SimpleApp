namespace Simple.Repository.Models.System;

/// <summary>
/// 用户表
/// </summary>
[Index(nameof(UserName), IsUnique = true)]
public class SysUser : BusinessEntityBase<Guid>, IConcurrency
{
    /// <summary>
    /// 用户名
    /// </summary>
    [MaxLength(64)]
    public string UserName { get; set; } = "";

    /// <summary>
    /// 密码
    /// </summary>
    [MaxLength(64)]
    public string Password { get; set; } = "";

    /// <summary>
    /// 姓名
    /// </summary>
    [MaxLength(32)]
    public string? Name { get; set; }

    /// <summary>
    /// 手机号码
    /// </summary>
    [MaxLength(16)]
    public string? Phone { get; set; }

    /// <summary>
    /// 邮件
    /// </summary>
    [MaxLength(64)]
    public string? Email { get; set; }

    /// <summary>
    /// 性别：1-男，2-女
    /// </summary>
    public GenderType Gender { get; set; } = GenderType.Unknown;

    /// <summary>
    /// 启用状态
    /// </summary>
    public bool IsEnabled { get; set; } = true;

    /// <summary>
    /// 主岗位Id
    /// </summary>
    public Guid? PositionId { get; set; }

    /// <summary>
    /// 主岗位
    /// </summary>
    public SysPosition? Position { get; set; }

    /// <summary>
    /// 主部门Id
    /// </summary>
    public Guid? OrganizationId { get; set; }

    /// <summary>
    /// 主部门
    /// </summary>
    public SysOrganization? Organization { get; set; }

    /// <summary>
    /// 用户角色
    /// </summary>
    public List<SysUserRole> UserRoles { get; set; } = new List<SysUserRole>();

    public long RowVersion { get; set; }

    public void SetPassword(string password)
    {
        Password = HashHelper.Md5(password);
    }
}
