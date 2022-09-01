using System;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using StackExchange.Redis;

namespace Simple.Services;

public class CacheService
{
    private const string UserCacheKey = "u_";
    private const string RoleCacheKey = "r_";


    #region 通用方法

    public async Task RemoveByPrefixAsync(string prefix)
    {
        HashSet<string> keySet = await CacheHelper.GetKeySetAsync();
        var keys = keySet.Where(key => key.StartsWith(prefix)).ToList();
        await CacheHelper.RemoveRangeAsync(keys);
    }

    public async Task<List<TCacheItem>> GetCacheItemsAsync<TCacheItem>(params string[] keys)
    {
        var result = new List<TCacheItem>();

        foreach (var key in keys)
        {
            var cacheItems = await CacheHelper.GetAsync<List<TCacheItem>>(key);
            if (cacheItems != null && cacheItems.Count > 0)
            {
                result.AddRange(cacheItems);
            }
        }

        return result;
    }

    public async Task SetCacheItemAsync<TCacheItem>(string key, List<TCacheItem> cacheItems)
    {
        await CacheHelper.SetAsync(key, cacheItems);
    }

    public async Task RemoveCacheAsync(string key)
    {
        await CacheHelper.RemoveAsync(key);
    }

    #endregion


    #region 清空缓存

    /// <summary>
    /// 清空以角色Id为索引键的缓存
    /// </summary>
    /// <param name="roleId">角色Id</param>
    /// <returns></returns>
    public async Task ClearRoleCacheAsync(Guid roleId)
    {
        await RemoveRoleApplicationAsync(roleId);
        await RemoveRoleMenuAsync(roleId);
        await RemoveRolePermissionAsync(roleId);
    }

    /// <summary>
    /// 清空 Menu 和 Permission 相关的缓存
    /// </summary>
    /// <returns></returns>
    public async Task ClearMenuAndPermissionCacheAsync()
    {
        await ClearMenuCacheAsync();
        await ClearPermissionCacheAsync();
    }

    /// <summary>
    /// 清空 Application 相关的缓存
    /// </summary>
    /// <returns></returns>
    public async Task ClearApplicationCacheAsync()
    {
        await RemoveByPrefixAsync(CacheKeyConst.Application);
    }

    /// <summary>
    /// 清空 Menu 相关的缓存
    /// </summary>
    /// <returns></returns>
    public async Task ClearMenuCacheAsync()
    {
        await RemoveByPrefixAsync(CacheKeyConst.Menu);
    }

    /// <summary>
    /// 清空 Permission 相关的缓存
    /// </summary>
    /// <returns></returns>
    public async Task ClearPermissionCacheAsync()
    {
        await RemoveByPrefixAsync(CacheKeyConst.Permission);
    }

    #endregion

    public async Task<List<ApplicationCacheItem>> GetUserApplicationsAsync(params Guid[] userIds)
    {
        string[] keys = userIds.Select(item => $"{CacheKeyConst.Application}{UserCacheKey}{item}").ToArray();
        return await GetCacheItemsAsync<ApplicationCacheItem>(keys);
    }

    public async Task<List<ApplicationCacheItem>> GetRoleApplicationsAsync(params Guid[] roleIds)
    {
        string[] keys = roleIds.Select(item => $"{CacheKeyConst.Application}{RoleCacheKey}{item}").ToArray();
        return await GetCacheItemsAsync<ApplicationCacheItem>(keys);
    }

    public async Task<List<MenuCacheItem>> GetUserMenusAsync(params Guid[] userIds)
    {
        string[] keys = userIds.Select(item => $"{CacheKeyConst.Menu}{UserCacheKey}{item}").ToArray();
        return await GetCacheItemsAsync<MenuCacheItem>(keys);
    }

    public async Task<List<MenuCacheItem>> GetRoleMenusAsync(params Guid[] roleIds)
    {
        string[] keys = roleIds.Select(item => $"{CacheKeyConst.Menu}{RoleCacheKey}{item}").ToArray();
        return await GetCacheItemsAsync<MenuCacheItem>(keys);
    }

    public async Task<List<string>> GetUserPermissionsAsync(params Guid[] userIds)
    {
        string[] keys = userIds.Select(item => $"{CacheKeyConst.Permission}{UserCacheKey}{item}").ToArray();
        return await GetCacheItemsAsync<string>(keys);
    }

    public async Task<List<string>> GetRolePermissionsAsync(params Guid[] roleIds)
    {
        string[] keys = roleIds.Select(item => $"{CacheKeyConst.Permission}{RoleCacheKey}{item}").ToArray();
        return await GetCacheItemsAsync<string>(keys);
    }

    public async Task SetUserApplicationAsync(Guid userId, List<ApplicationCacheItem> applications)
    {
        var key = $"{CacheKeyConst.Application}{UserCacheKey}{userId}";
        await SetCacheItemAsync(key, applications);
    }

    public async Task SetRoleApplicationAsync(Guid roleId, List<ApplicationCacheItem> applications)
    {
        var key = $"{CacheKeyConst.Application}{RoleCacheKey}{roleId}";
        await SetCacheItemAsync(key, applications);
    }

    public async Task SetUserMenuAsync(Guid userId, List<MenuCacheItem> menus)
    {
        var key = $"{CacheKeyConst.Menu}{UserCacheKey}{userId}";
        await SetCacheItemAsync(key, menus);
    }

    public async Task SetRoleMenuAsync(Guid roleId, List<MenuCacheItem> menus)
    {
        var key = $"{CacheKeyConst.Menu}{RoleCacheKey}{roleId}";
        await SetCacheItemAsync(key, menus);
    }

    public async Task SetUserPermissionAsync(Guid userId, List<string> permissions)
    {
        var key = $"{CacheKeyConst.Permission}{UserCacheKey}{userId}";
        await SetCacheItemAsync(key, permissions);
    }

    public async Task SetRolePermissionAsync(Guid roleId, List<string> permissions)
    {
        var key = $"{CacheKeyConst.Permission}{RoleCacheKey}{roleId}";
        await SetCacheItemAsync(key, permissions);
    }

    public async Task RemoveUserApplicationAsync(Guid userId)
    {
        var key = $"{CacheKeyConst.Application}{UserCacheKey}{userId}";
        await RemoveCacheAsync(key);
    }

    public async Task RemoveRoleApplicationAsync(Guid roleId)
    {
        var key = $"{CacheKeyConst.Application}{RoleCacheKey}{roleId}";
        await RemoveCacheAsync(key);
    }

    public async Task RemoveUserMenuAsync(Guid userId)
    {
        var key = $"{CacheKeyConst.Menu}{UserCacheKey}{userId}";
        await RemoveCacheAsync(key);
    }

    public async Task RemoveRoleMenuAsync(Guid roleId)
    {
        var key = $"{CacheKeyConst.Menu}{RoleCacheKey}{roleId}";
        await RemoveCacheAsync(key);
    }

    public async Task RemoveUserPermissionAsync(Guid userId)
    {
        var key = $"{CacheKeyConst.Permission}{UserCacheKey}{userId}";
        await RemoveCacheAsync(key);
    }

    public async Task RemoveRolePermissionAsync(Guid roleId)
    {
        var key = $"{CacheKeyConst.Permission}{RoleCacheKey}{roleId}";
        await RemoveCacheAsync(key);
    }
}
