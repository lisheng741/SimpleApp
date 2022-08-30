namespace Simple.Services;

public class UserInfoRoleModel : ModelBase
{
    /// <summary>
    /// 主键Id
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// 编码
    /// </summary>
    public string Code { get; set; } = "";

    /// <summary>
    /// 角色名
    /// </summary>
    public string Name { get; set; } = "";


    public override void ConfigureMapper(Profile profile)
    {
        profile.CreateMap<SysRole, UserInfoRoleModel>();
    }
}
