namespace Simple.Services;

public class UserGrantRoleInputModel
{
    /// <summary>
    /// 用户Id
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// 拥有的角色Id列表
    /// </summary>
    public Guid[] GrantRoleIdList { get; set; } = new Guid[0];
}
