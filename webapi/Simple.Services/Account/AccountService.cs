using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Simple.Common.Authentication;
using Simple.Common.Authentication.Jwt;
using StackExchange.Redis;

namespace Simple.Services;

public class AccountService
{
    private readonly SimpleDbContext _context;
    private readonly ISimpleService _simpleService;
    private readonly ICurrentUserService _currentUser;
    private readonly CacheService _cacheService;
    private readonly UserService _userService;

    public AccountService(SimpleDbContext context,
                          ISimpleService simpleService,
                          ICurrentUserService currentUser,
                          CacheService cacheService,
                          UserService userService,
                          RoleService roleService)
    {
        _context = context;
        _simpleService = simpleService;
        _currentUser = currentUser;
        _userService = userService;
        _cacheService = cacheService;
    }

    /// <summary>
    /// 获取 JwtToken
    /// </summary>
    /// <param name="login"></param>
    /// <returns></returns>
    public async Task<string> GetTokenAsync(LoginModel login)
    {
        var passwordHash = HashHelper.Md5(login.Password);

        var user = await _context.Set<SysUser>()
            .Include(u => u.UserRoles)
            .Where(u => u.UserName == login.Account)
            .Where(u => u.Password == passwordHash)
            .FirstOrDefaultAsync();

        if(user == null)
        {
            throw AppResultException.Status404NotFound("用户不存在或密码不匹配");
        }

        if (!user.IsEnabled)
        {
            throw AppResultException.Status403Forbidden("该账号已被停用");
        }

        // 用户信息
        List<Claim> claims = new List<Claim>()
        {
            new Claim(SimpleClaimTypes.UserId, user.Id.ToString()),
            new Claim(SimpleClaimTypes.Name, user.Name ?? ""),
            new Claim(SimpleClaimTypes.Email, user.Email?? ""),
            new Claim(SimpleClaimTypes.AdminType, user.AdminType.ToString()),
        };
        string[] roles = user.UserRoles.Select(ur => ur.RoleId.ToString()).ToArray();

        // 生成 token
        var jwtTokenModel = new JwtTokenModel(login.Account, claims, roles);
        string token = JwtHelper.Create(jwtTokenModel);

        return token;
    }

    /// <summary>
    /// 获取用户信息（适配小诺1.8 vue前端）
    /// </summary>
    /// <returns></returns>
    public async Task<UserInfoModel> GetUserInfoAsync()
    {
        if (_currentUser.UserId == null)
        {
            throw AppResultException.Status401Unauthorized();
        }

        var result = new UserInfoModel();

        // 关联 UserRole 查询用户
        var user = await _context.Set<SysUser>()
            .Include(u => u.UserRoles).ThenInclude(ur => ur.Role)
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
        List<SysRole> roles = user.UserRoles.Where(ur => ur.Role != null).Select(ur => ur.Role!).ToList();
        result.Roles = MapperHelper.Map<List<UserInfoRoleModel>>(roles);

        // Apps 应用信息
        result.Apps = await GetUserApplicationsAsync(user);
        // 如果没有默认应用，设定第一个为默认应用
        if (result.Apps.Count > 0 && !(result.Apps.Any(a => a.Active)))
        {
            result.Apps[0].Active = true;
        }

        // Menus 菜单信息
        result.Menus = await GetUserMenusAsync(user);

        // Permissions 权限信息
        result.Permissions = await GetUserPermissionsAsync(user);

        return result;
    }

    /// <summary>
    /// 当前用户是否为超级管理员
    /// </summary>
    /// <returns></returns>
    public bool IsSuperAdmin()
    {
        return _currentUser.IsSuperAdmin;
    }

    /// <summary>
    /// 获取当前用户应用列表
    /// </summary>
    /// <returns></returns>
    public async Task<List<UserInfoApplicationModel>> GetUserApplicationsAsync()
    {
        // 通过构建一个 SysUser 对象，来获取结果
        SysUser? user = BuildCurrentUser();
        if (user == null)
        {
            // 如果是在权限过滤器中抛出，将不会被处理为返回结果，因为 AuthorizationFilter 先于 ActionFilter
            //throw AppResultException.Status401Unauthorized("用户不存在");

            // 直接返回空结果，表示没有任何权限
            return new List<UserInfoApplicationModel>();
        }

        // 获取结果
        return await GetUserApplicationsAsync(user);
    }

    /// <summary>
    /// 获取用户应用列表
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<List<UserInfoApplicationModel>> GetUserApplicationsAsync(Guid userId)
    {
        // 关联 UserRole 查询用户
        var user = await _context.Set<SysUser>()
            .Include(u => u.UserRoles).ThenInclude(ur => ur.Role)
            .Where(u => u.Id == userId)
            .FirstOrDefaultAsync();

        if (user == null)
        {
            throw AppResultException.Status404NotFound("用户不存在");
        }

        return await GetUserApplicationsAsync(user);
    }

    /// <summary>
    /// 获取用户应用列表
    /// </summary>
    /// <param name="user">包含角色信息的用户数据</param>
    /// <returns></returns>
    public async Task<List<UserInfoApplicationModel>> GetUserApplicationsAsync(SysUser user)
    {
        if (user.AdminType == AdminType.SuperAdmin)
        {
            // 超级管理员用户
            // 优先读缓存
            List<ApplicationCacheItem> applicationCacheItems = await _cacheService.GetUserApplicationsAsync(user.Id);
            if (applicationCacheItems.Count > 0)
            {
                // 去重并排序
                applicationCacheItems = applicationCacheItems.Distinct().OrderBy(a => a.Sort).ToList();

                // 返回结果
                return MapperHelper.Map<List<UserInfoApplicationModel>>(applicationCacheItems);
            }

            // 没有缓存，从数据库读取所有应用
            var applications = _context.Set<SysApplication>()
                .OrderBy(a => a.Sort)
                .Distinct()
                .ToList();

            // 缓存管理员用户应用信息
            applicationCacheItems = MapperHelper.Map<List<ApplicationCacheItem>>(applications);
            await _cacheService.SetUserApplicationAsync(user.Id, applicationCacheItems);

            // 返回结果集
            return MapperHelper.Map<List<UserInfoApplicationModel>>(applications);
        }
        else
        {
            // 其他用户
            // 获取用户角色列表
            var roleIds = user.UserRoles.Select(ur => ur.RoleId);

            // 返回角色应用列表
            return await GetRoleApplicationsAsync(roleIds.ToArray());
        }
    }

    /// <summary>
    /// 获取角色应用列表
    /// </summary>
    /// <param name="roleIds"></param>
    /// <returns></returns>
    public async Task<List<UserInfoApplicationModel>> GetRoleApplicationsAsync(params Guid[] roleIds)
    {

        // 优先读取缓存
        List<ApplicationCacheItem> applicationCacheItems = await _cacheService.GetRoleApplicationsAsync(roleIds);
        if (applicationCacheItems.Count > 0)
        {
            // 去重并排序
            applicationCacheItems = applicationCacheItems.Distinct().OrderBy(a => a.Sort).ToList();

            // 返回结果
            return MapperHelper.Map<List<UserInfoApplicationModel>>(applicationCacheItems);
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
            .OrderBy(a => a.Sort)
            .Distinct()
            .ToList();

        // step4: 缓存
        foreach (var role in roles)
        {
            List<string> roleApplicationCodes = role.RoleMenus
                .Where(rm => rm.Menu != null).Select(rm => rm.Menu!)
                .Where(m => m.Application != null).Select(m => m!.Application!).ToList();

            List<SysApplication> roleApplications = applications.Where(a => applicationCodes.Contains(a.Code)).ToList();

            if (roleApplications.Any())
            {
                // 添加缓存
                var roleApplicationCacheItems = MapperHelper.Map<List<ApplicationCacheItem>>(roleApplications);
                await _cacheService.SetRoleApplicationAsync(role.Id, roleApplicationCacheItems);
            }
        }

        return MapperHelper.Map<List<UserInfoApplicationModel>>(applications);
    }

    /// <summary>
    /// 获取当前用户菜单列表（AntDesignPro菜单）
    /// </summary>
    /// <returns></returns>
    public async Task<List<UserInfoMenuModel>> GetUserMenusAsync()
    {
        // 通过构建一个 SysUser 对象，来获取结果
        SysUser? user = BuildCurrentUser();
        if (user == null)
        {
            // 如果是在权限过滤器中抛出，将不会被处理为返回结果，因为 AuthorizationFilter 先于 ActionFilter
            //throw AppResultException.Status401Unauthorized("用户不存在");

            // 直接返回空结果，表示没有任何权限
            return new List<UserInfoMenuModel>();
        }

        // 获取结果
        return await GetUserMenusAsync(user);
    }

    /// <summary>
    /// 获取用户菜单列表（AntDesignPro菜单）
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<List<UserInfoMenuModel>> GetUserMenusAsync(Guid userId)
    {
        // 关联 UserRole 查询用户
        var user = await _context.Set<SysUser>()
            .Include(u => u.UserRoles).ThenInclude(ur => ur.Role)
            .Where(u => u.Id == userId)
            .FirstOrDefaultAsync();

        if (user == null)
        {
            throw AppResultException.Status404NotFound("用户不存在");
        }

        return await GetUserMenusAsync(user);
    }

    /// <summary>
    /// 获取用户菜单列表（AntDesignPro菜单）
    /// </summary>
    /// <param name="user">包含角色信息的用户数据</param>
    /// <returns></returns>
    public async Task<List<UserInfoMenuModel>> GetUserMenusAsync(SysUser user)
    {
        if (user.AdminType == AdminType.SuperAdmin)
        {
            // 超级管理员用户
            // 优先读缓存
            List<MenuCacheItem> menuCacheItems = await _cacheService.GetUserMenusAsync(user.Id);
            if (menuCacheItems.Count > 0)
            {
                return MapperHelper.Map<List<UserInfoMenuModel>>(menuCacheItems.Distinct());
            }

            // 从数据库读取菜单列表
            var menus = await _context.Set<SysMenu>()
                .Where(m => m.IsEnabled)
                .Where(m => m.Type != MenuType.Button)
                .OrderBy(m => m.Sort).ThenBy(m => m.Id)
                .ToListAsync();

            // 缓存
            menuCacheItems = MapperHelper.Map<List<MenuCacheItem>>(menus);
            await _cacheService.SetUserMenuAsync(user.Id, menuCacheItems);

            // 返回结果
            return MapperHelper.Map<List<UserInfoMenuModel>>(menus);
        }
        else
        {
            // 其他用户
            // 获取用户角色列表
            var roleIds = user.UserRoles.Select(ur => ur.RoleId);

            // 返回角色菜单列表
            return await GetRoleMenusAsync(roleIds.ToArray());
        }
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
    /// 获取所有权限
    /// </summary>
    /// <returns></returns>
    public async Task<List<string>> GetAllPermissionsAsync()
    {
        return await GetUserPermissionsAsync(new SysUser() { Id = Guid.Empty});
    }

    /// <summary>
    /// 获取当前用户权限列表（钮权限标识）
    /// </summary>
    /// <returns></returns>
    public async Task<List<string>> GetUserPermissionsAsync()
    {
        // 通过构建一个 SysUser 对象，来获取结果

        SysUser? user = BuildCurrentUser();
        if (user == null)
        {
            // 如果是在权限过滤器中抛出，将不会被处理为返回结果，因为 AuthorizationFilter 先于 ActionFilter
            //throw AppResultException.Status401Unauthorized("用户不存在");

            // 直接返回空结果，表示没有任何权限
            return new List<string>();
        }

        // 获取结果
        return await GetUserPermissionsAsync(user);
    }

    /// <summary>
    /// 获取用户权限列表（钮权限标识）
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<List<string>> GetUserPermissionsAsync(Guid userId)
    {
        // 关联 UserRole 查询用户
        var user = await _context.Set<SysUser>()
            .Include(u => u.UserRoles).ThenInclude(ur => ur.Role)
            .Where(u => u.Id == userId)
            .FirstOrDefaultAsync();

        if (user == null)
        {
            throw AppResultException.Status404NotFound("用户不存在");
        }

        return await GetUserPermissionsAsync(user);
    }

    /// <summary>
    /// 获取用户权限列表（钮权限标识）
    /// </summary>
    /// <param name="user">包含角色信息的用户数据</param>
    /// <returns></returns>
    private async Task<List<string>> GetUserPermissionsAsync(SysUser user)
    {
        if(user.AdminType == AdminType.SuperAdmin || user.Id == Guid.Empty)
        {
            List<string> result = await _cacheService.GetUserPermissionsAsync(user.Id);
            if (result.Count > 0)
            {
                return result.Distinct().ToList();
            }

            List<string> permissions = await _context.Set<SysMenu>()
                .Where(m => m.IsEnabled)
                .Where(m => m.Type == MenuType.Button)
                .Where(m => !string.IsNullOrEmpty(m.Permission))
                .Select(m => m.Permission!)
                .Distinct()
                .ToListAsync();

            await _cacheService.SetUserPermissionAsync(user.Id, permissions);

            return permissions;
        }
        else
        {
            var roleIds = user.UserRoles.Select(ur => ur.RoleId);

            return await GetRolePermissionsAsync(roleIds.ToArray());
        }
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

    private SysUser? BuildCurrentUser()
    {
        if (!_currentUser.UserId.HasValue)
        {
            return default;
        }

        // 用户角色信息
        List<SysUserRole> userRoles = new List<SysUserRole>();
        foreach (var roleId in _currentUser.Roles)
        {
            var userRole = new SysUserRole()
            {
                UserId = _currentUser.UserId.Value,
                RoleId = new Guid(roleId),
            };
            userRoles.Add(userRole);
        }

        // 构建一个 SysUser 对象
        SysUser user = new SysUser()
        {
            Id = _currentUser.UserId.Value,
            UserRoles = userRoles
        };

        return user;
    }
}
