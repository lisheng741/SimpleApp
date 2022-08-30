using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace Simple.Repository.Models.System;

/// <summary>
/// 角色表
/// </summary>
public class SysRole : BusinessEntityBase<Guid>, IConcurrency
{
    /// <summary>
    /// 编码
    /// </summary>
    [MaxLength(128)]
    public string Code { get; set; } = "";

    /// <summary>
    /// 名称
    /// </summary>
    [MaxLength(128)]
    public string Name { get; set; } = "";

    /// <summary>
    /// 排序
    /// </summary>
    public int Sort { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [MaxLength(2048)]
    public string? Remark { get; set; }

    /// <summary>
    /// 启用状态
    /// </summary>
    public bool IsEnabled { get; set; } = true;

    /// <summary>
    /// 数据范围（1-全部数据，2-本部门及以下数据，3-本部门数据，4-仅本人数据，5-自定义数据）
    /// </summary>
    public DataScopeType DataScope { get; set; } = DataScopeType.All;

    /// <summary>
    /// 用户角色
    /// </summary>
    public List<SysUserRole> UserRoles { get; set; } = new List<SysUserRole>();

    /// <summary>
    /// 角色菜单
    /// </summary>
    public List<SysRoleMenu> RoleMenus { get; set; } = new List<SysRoleMenu>();

    /// <summary>
    /// 角色数据范围
    /// </summary>
    public List<SysRoleDataScope> RoleDataScopes { get; set; } = default!;

    public long RowVersion { get; set; }

    public SysRole()
    {
    }

    public void AddMenu(params Guid[] menuIds)
    {
        foreach (var menuId in menuIds)
        {
            // 存在则跳过
            if(RoleMenus.Any(rm => rm.MenuId == menuId))
            {
                continue;
            }

            // 新增
            var roleMenu = new SysRoleMenu()
            {
                RoleId = Id,
                MenuId = menuId
            };
            RoleMenus.Add(roleMenu);
        }
    }

    public void RemoveMenu(params Guid[] menuIds)
    {
        foreach (var menuId in menuIds)
        {
            var roleMenu = RoleMenus.Where(rm => rm.MenuId == menuId).FirstOrDefault();
            if (roleMenu != null)
            {
                RoleMenus.Remove(roleMenu);
            }
        }
    }

    public void SetMenu(params Guid[] menuIds)
    {
        var oldMenuIds = RoleMenus.Select(rm => rm.MenuId);

        var removeMenuIds = oldMenuIds.Except(menuIds);
        var addMenuIds = menuIds.Except(oldMenuIds);

        RemoveMenu(removeMenuIds.ToArray());
        AddMenu(addMenuIds.ToArray());
    }

    public void AddDataScope(params Guid[] organizationIds)
    {
        foreach(var organizationId in organizationIds)
        {
            if(RoleDataScopes.Any(rd => rd.OrganizationId == organizationId))
            {
                continue;
            }

            var dataScope = new SysRoleDataScope()
            {
                RoleId = Id,
                OrganizationId = organizationId
            };
            RoleDataScopes.Add(dataScope);
        }
    }

    public void RemoveDataScope(params Guid[] organizationIds)
    {
        foreach(var organizationId in organizationIds)
        {
            var dataScope = RoleDataScopes.Where(rd => rd.OrganizationId == organizationId).FirstOrDefault();

            if(dataScope != null)
            {
                RoleDataScopes.Remove(dataScope);
            }
        }
    }

    public void SetDataScope(params Guid[] organizationIds)
    {
        var oldOrganizationIds = RoleDataScopes.Select(rd => rd.OrganizationId);

        var removeOrganizationIds = oldOrganizationIds.Except(organizationIds);
        var addOrganizationIds = organizationIds.Except(oldOrganizationIds);

        RemoveDataScope(removeOrganizationIds.ToArray());
        AddDataScope(addOrganizationIds.ToArray());
    }


    public override void ConfigureEntity(ModelBuilder builder)
    {
        // DataScope 默认值 1
        builder.Entity<SysRole>().Property(r => r.DataScope).HasDefaultValue(DataScopeType.All);
    }
}
