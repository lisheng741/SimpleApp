using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Simple.Common.Authentication;
using Simple.Common.Authentication.Jwt;
using Simple.Common.DependencyInjection;

namespace Simple.Services;

public class AccountService
{
    private readonly SimpleDbContext _context;
    private readonly ICurrentUserService _currentUser;
    private readonly CacheService _cacheService;
    private readonly UserService _userService;

    public AccountService(SimpleDbContext context,
                          ICurrentUserService currentUser,
                          CacheService cacheService,
                          UserService userService,
                          RoleService roleService)
    {
        _context = context;
        _currentUser = currentUser;
        _userService = userService;
        _cacheService = cacheService;
    }

    public Task<string> CreateTokenAsync(LoginModel login)
    {
        List<Claim> claims = new List<Claim>()
        {
            new Claim(SimpleClaimTypes.UserId, "08da8458-c8ce-4a1e-8924-b5377d708ff5")
        };
        var jwtTokenModel = new JwtTokenModel(login.Account, claims, "admin");
        string token = JwtHelper.Create(jwtTokenModel);

        return Task.FromResult(token);
    }

    public async Task<UserInfoModel> GetUserInfoAsync()
    {
        if (_currentUser.UserId == null)
        {
            throw AppResultException.Status401Unauthorized();
        }

        var result = new UserInfoModel();

        // 查询用户
        var user = await _context.Set<SysUser>()
            .Include(u => u.UserRoles).ThenInclude(ur => ur.Role).ThenInclude(r => r!.RoleMenus).ThenInclude(rm => rm.Menu)
            .Include(u => u.UserDataScopes)
            .Where(u => u.Id == _currentUser.UserId)
            .FirstOrDefaultAsync();

        if (user == null)
        {
            throw AppResultException.Status404NotFound("???");
        }

        // SysUser 映射为 UserInfoModel
        MapperHelper.Map(user, result);

        // 角色Id 列表
        var roleIds = user.UserRoles.Select(ur => ur.RoleId);

        // Roles 角色信息
        List<SysRole> roles = user.UserRoles.Where(ur => ur.Role != null).Select(ur => ur.Role!).ToList(); ;
        result.Roles = MapperHelper.Map<List<UserInfoRoleModel>>(roles);

        // Menus 菜单信息
        result.Menus = await GetRoleMenusAsync(roleIds.ToArray());

        // Permissions 权限信息
        result.Permissions = await GetRolePermissionsAsync(roleIds.ToArray());

        // Apps 应用信息
        result.Apps = await GetRoleApplicationsAsync(roleIds.ToArray());
        if (result.Apps.Count > 0 && !(result.Apps.Any(a => a.Active)))
        {
            result.Apps[0].Active = true;
        }


        //result.AdminType = 1;

        return result;
    }

    /// <summary>
    /// 获取角色菜单列表（AntDesignPro菜单）
    /// </summary>
    /// <param name="roleIds"></param>
    /// <returns></returns>
    public async Task<List<UserInfoMenuModel>> GetRoleMenusAsync(params Guid[] roleIds)
    {
        // 优先读取缓存
        List<MenuCacheItem> menuCacheItems = await _cacheService.GetRoleMenusAsync(roleIds);
        if (menuCacheItems.Count > 0)
        {
            return MapperHelper.Map<List<UserInfoMenuModel>>(menuCacheItems.Distinct());
        }

        // 没有缓存，读数据库
        List<SysMenu> menus = new List<SysMenu>();
        var roles = await _context.Set<SysRole>()
            .Include(r => r.RoleMenus.Where(rm => rm.Menu!.IsEnabled)
                                     .Where(rm => rm.Menu!.Type != MenuType.Button)
            )
            .ThenInclude(rm => rm.Menu)
            .Where(r => roleIds.Contains(r.Id))
            .ToListAsync();

        foreach (var role in roles)
        {
            List<SysMenu> roleMenus = role.RoleMenus
                .Where(rm => rm.Menu != null)
                .Select(rm => rm.Menu!)
                .ToList();

            if (roleMenus.Count > 0)
            {
                // 添加到列表
                menus.AddRange(roleMenus);

                // 添加缓存
                var roleMenuCacheItems = MapperHelper.Map<List<MenuCacheItem>>(roleMenus);
                await _cacheService.SetRoleMenuAsync(role.Id, roleMenuCacheItems);
            }
        }

        return MapperHelper.Map<List<UserInfoMenuModel>>(menus);
    }

    /// <summary>
    /// 获取角色权限列表（钮权限标识）
    /// </summary>
    /// <param name="roleIds"></param>
    /// <returns></returns>
    public async Task<List<string>> GetRolePermissionsAsync(params Guid[] roleIds)
    {
        // 优先从缓存获取
        List<string> result = await _cacheService.GetRolePermissionsAsync(roleIds);
        if (result.Count > 0)
        {
            return result.Distinct().ToList();
        }

        // 从数据库读取
        var roles = await _context.Set<SysRole>()
            .Include(r => r.RoleMenus.Where(rm => rm.Menu!.IsEnabled)
                                     .Where(rm => rm.Menu!.Type == MenuType.Button)
            )
            .ThenInclude(rm => rm.Menu)
            .Where(r => roleIds.Contains(r.Id))
            .ToListAsync();

        foreach (var role in roles)
        {
            var permissions = role.RoleMenus
                .Where(rm => rm.Menu != null)
                .Select(rm => rm.Menu!)
                .Where(m => !string.IsNullOrEmpty(m.Permission))
                .Select(m => m.Permission!)
                .Distinct()
                .ToList();

            if (permissions.Count > 0)
            {
                // 添加到结果列表
                result.AddRange(permissions);

                // 添加缓存
                await _cacheService.SetRolePermissionAsync(role.Id, permissions);
            }
        }

        return result;
    }

    public async Task<List<UserInfoApplicationModel>> GetRoleApplicationsAsync(params Guid[] roleIds)
    {

        // 优先读取缓存
        List<ApplicationCacheItem> applicationCacheItems = await _cacheService.GetRoleApplicationsAsync(roleIds);
        if (applicationCacheItems.Count > 0)
        {
            return MapperHelper.Map<List<UserInfoApplicationModel>>(applicationCacheItems.Distinct());
        }

        // 没有缓存，读数据库
        // step1: 获取角色拥有的菜单
        var roles = await _context.Set<SysRole>()
            .Include(r => r.RoleMenus.Where(rm => rm.Menu!.IsEnabled)
                                     .Where(rm => rm.Menu!.Type != MenuType.Button)
            )
            .ThenInclude(rm => rm.Menu)
            .Where(r => roleIds.Contains(r.Id))
            .ToListAsync();

        // step2: 获取 ApplicationCode 列表
        List<string> applicationCodes = roles.SelectMany(r => r!.RoleMenus)
            .Where(rm => rm.Menu != null).Select(rm => rm.Menu!)
            .Where(m => m.Application != null).Select(m => m!.Application!).ToList();
            
        // step3: 获取 Application 列表
        var applications = _context.Set<SysApplication>()
            .Where(a => applicationCodes.Contains(a.Code))
            .ToList();

        // step4: 缓存
        foreach(var role in roles)
        {
            List<string> roleApplicationCodes = role.RoleMenus
                .Where(rm => rm.Menu != null).Select(rm => rm.Menu!)
                .Where(m => m.Application != null).Select(m => m!.Application!).ToList();

            List<SysApplication> roleApplications = applications.Where(a => applicationCodes.Contains(a.Code)).ToList();

            if(roleApplications.Any())
            {
                // 添加缓存
                var roleApplicationCacheItems = MapperHelper.Map<List<ApplicationCacheItem>>(roleApplications);
                await _cacheService.SetRoleApplicationAsync(role.Id, roleApplicationCacheItems);
            }
        }

        return MapperHelper.Map<List<UserInfoApplicationModel>>(applications);
    }
}
