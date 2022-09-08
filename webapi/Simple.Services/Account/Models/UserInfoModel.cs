namespace Simple.Services;

public class UserInfoModel : ModelBase
{
    /// <summary>
    /// 主键
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// 账号
    /// </summary>
    public string Account { get; set; } = "";

    /// <summary>
    /// 姓名
    /// </summary>
    public string Name { get; set; } = "";

    /// <summary>
    /// 昵称
    /// </summary>
    public string? NickName { get; set; }

    /// <summary>
    /// 头像
    /// </summary>
    public string? Avatar { get; set; }

    /// <summary>
    /// 生日
    /// </summary>
    public DateTimeOffset? Birthday { get; set; }

    /// <summary>
    /// 性别(1-男, 2-女)
    /// </summary>
    public GenderType Sex { get; set; }

    /// <summary>
    /// 邮箱
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// 手机
    /// </summary>
    public string? Phone { get; set; }

    /// <summary>
    /// 电话
    /// </summary>
    public string? Tel { get; set; }

    /// <summary>
    /// 管理员类型（1-超级管理员, 2-管理员, 3-普通账号）
    /// </summary>
    public AdminType AdminType { get; set; } = AdminType.None;

    /// <summary>
    /// 最后登陆IP
    /// </summary>
    public string? LastLoginIp { get; set; }

    /// <summary>
    /// 最后登陆时间
    /// </summary>
    public DateTimeOffset? LastLoginTime { get; set; }

    /// <summary>
    /// 最后登陆地址
    /// </summary>
    public string? LastLoginAddress { get; set; }

    /// <summary>
    /// 最后登陆所用浏览器
    /// </summary>
    public string? LastLoginBrowser { get; set; }

    /// <summary>
    /// 最后登陆所用系统
    /// </summary>
    public string? LastLoginOs { get; set; }

    ///// <summary>
    ///// 员工信息
    ///// </summary>
    //public EmpOutput LoginEmpInfo { get; set; } = new EmpOutput();

    /// <summary>
    /// 具备应用信息
    /// </summary>
    public List<UserInfoApplicationModel> Apps { get; set; } = new List<UserInfoApplicationModel>();

    /// <summary>
    /// 角色信息
    /// </summary>
    public List<UserInfoRoleModel> Roles { get; set; } = new List<UserInfoRoleModel>();

    /// <summary>
    /// 权限信息
    /// </summary>
    public List<string> Permissions { get; set; } = new List<string>();

    /// <summary>
    /// 系统所有权限信息
    /// </summary>
    public List<string> AllPermissions { get; set; } = new List<string>();

    /// <summary>
    /// 登录菜单信息（AntDesign）
    /// </summary>
    public List<UserInfoMenuModel> Menus { get; set; } = new List<UserInfoMenuModel>();

    /// <summary>
    /// 数据范围（机构）信息
    /// </summary>
    public List<Guid> DataScopes { get; set; } = new List<Guid>();


    public override void ConfigureMapper(Profile profile)
    {
        profile.CreateMap<SysUser, UserInfoModel>()
            .ForMember(d => d.Account, options => options.MapFrom(s => s.UserName))
            .ForMember(d => d.NickName, options => options.MapFrom(s => s.Name))
            .ForMember(d => d.Sex, options => options.MapFrom(s => s.Gender));
    }
}
