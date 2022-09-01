using System.Linq;

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
    /// 账号类型（1-超级管理员，2-管理员，3-普通账号）
    /// </summary>
    public AdminType AdminType { get; set; } = AdminType.None;

    /// <summary>
    /// 用户角色
    /// </summary>
    public List<SysUserRole> UserRoles { get; set; } = new List<SysUserRole>();

    /// <summary>
    /// 用户数据范围
    /// </summary>
    public List<SysUserDataScope> UserDataScopes { get; set; } = default!;

    public long RowVersion { get; set; }

    public SysUser()
    {

    }

    public void SetPassword(string password)
    {
        Password = HashHelper.Md5(password);
    }

    public void AddRole(params Guid[] roleIds)
    {
        foreach(var roleId in roleIds)
        {
            if(UserRoles.Any(ur => ur.RoleId == roleId))
            {
                continue;
            }

            var userRole = new SysUserRole()
            {
                UserId = Id,
                RoleId = roleId
            };
            UserRoles.Add(userRole);
        }
    }

    public void RemoveRole(params Guid[] roleIds)
    {
        foreach(var roleId in roleIds)
        {
            var userRole = UserRoles.FirstOrDefault(ur => ur.RoleId == roleId);
            if(userRole != null)
            {
                UserRoles.Remove(userRole);
            }
        }
    }

    public void SetRole(params Guid[] roleIds)
    {
        var oldRoleIds = UserRoles.Select(ur => ur.RoleId);

        var removeRoleIds = oldRoleIds.Except(roleIds);
        var addRoleIds = roleIds.Except(oldRoleIds);

        RemoveRole(removeRoleIds.ToArray());
        AddRole(addRoleIds.ToArray());
    }

    public void AddDataScope(params Guid[] organizationIds)
    {
        foreach (var organizationId in organizationIds)
        {
            if (UserDataScopes.Any(rd => rd.OrganizationId == organizationId))
            {
                continue;
            }

            var dataScope = new SysUserDataScope()
            {
                UserId = Id,
                OrganizationId = organizationId
            };
            UserDataScopes.Add(dataScope);
        }
    }

    public void RemoveDataScope(params Guid[] organizationIds)
    {
        foreach (var organizationId in organizationIds)
        {
            var dataScope = UserDataScopes.Where(rd => rd.OrganizationId == organizationId).FirstOrDefault();

            if (dataScope != null)
            {
                UserDataScopes.Remove(dataScope);
            }
        }
    }

    public void SetDataScope(params Guid[] organizationIds)
    {
        var oldOrganizationIds = UserDataScopes.Select(rd => rd.OrganizationId);

        var removeOrganizationIds = oldOrganizationIds.Except(organizationIds);
        var addOrganizationIds = organizationIds.Except(oldOrganizationIds);

        RemoveDataScope(removeOrganizationIds.ToArray());
        AddDataScope(addOrganizationIds.ToArray());
    }


    public override void ConfigureEntity(ModelBuilder builder)
    {
        // 配置关系：组织/岗位的删除，不能影响用户表
        builder.Entity<SysUser>()
            .HasOne(u => u.Organization)
            .WithMany()
            .OnDelete(DeleteBehavior.SetNull);
        builder.Entity<SysUser>()
            .HasOne(u => u.Position)
            .WithMany()
            .OnDelete(DeleteBehavior.SetNull);

        // AdminType 默认值 3
        builder.Entity<SysUser>().Property(r => r.AdminType).HasDefaultValue(AdminType.None);
    }
}
