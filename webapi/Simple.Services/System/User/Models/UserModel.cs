namespace Simple.Services;

public class UserModel : ModelBase
{
    /// <summary>
    /// 主键
    /// </summary>
    public Guid? Id { get; set; }

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

    public UserModel(string userName, string password)
    {
        UserName = userName;
        Password = password;
    }

    public override void ConfigureMapper(Profile profile)
    {
        profile.CreateMap<SysUser, UserModel>();

        profile.CreateMap<UserModel, SysUser>()
            .ForMember(d => d.Id, options => options.Ignore());
    }
}
