namespace Simple.Services;

public class RoleGrantMenuInputModel
{
    /// <summary>
    /// 角色Id
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// 拥有的菜单Id列表
    /// </summary>
    public Guid[] GrantMenuIdList { get; set; } = new Guid[0];
}
