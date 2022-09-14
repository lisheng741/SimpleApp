using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Simple.Common.Authorization;

namespace Simple.Services.Permissions;

public class PermissionChecker : IPermissionChecker
{
    private readonly ISimpleService _simpleService;
    private readonly AccountService _accountService;

    public PermissionChecker(ISimpleService simpleService, 
                             AccountService accountService)
    {
        _simpleService = simpleService;
        _accountService = accountService;
    }

    public virtual async Task<bool> IsGrantedAsync(ClaimsPrincipal claimsPrincipal, string name)
    {
        // 如果当前用户是超级管理员，跳过验证
        if (_accountService.IsSuperAdmin()) return true;

        string permission = name;

        if (string.IsNullOrEmpty(permission))
        {
            var request = _simpleService.HttpContext.Request;

            // 路径形如：/api/sysMenu/add 转化为 api:sysMenu:add，下面这两句是等价的
            //permission = request.Path.Value?.Substring(1).ToLower().Replace("/", ":");
            //permission = request.Path.Value?[1..].ToLower().Replace("/", ":") ?? "";

            // 路径形如：/api/sysMenu/add 转化为 sysmenu:add
            permission = request.Path.Value?.ToLower().Replace("/api/", "").Replace("/", ":") ?? "";
        }

        if (string.IsNullOrEmpty(permission)) return false;

        // 获取录入系统中的所有权限
        List<string> allPermissions = await _accountService.GetAllPermissionsAsync();

        // 如果没有配置该权限，则不限制该权限，通过验证
        if (!allPermissions.Contains(permission)) return true;

        // 获取当前用户的所有权限
        List<string> permissions = await _accountService.GetUserPermissionsAsync();

        // 如果当前用户拥有对应权限，则通过验证
        if (permissions.Contains(permission)) return true;

        return false;
    }
}
