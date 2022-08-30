namespace Simple.Services;

public class CacheService
{
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

    public async Task<List<ApplicationCacheItem>> GetRoleApplicationsAsync(params Guid[] roleIds)
    {
        var result = new List<ApplicationCacheItem>();

        foreach (var roleId in roleIds)
        {
            var key = $"{CacheKeyConst.Application}{roleId}";
            var applications = await CacheHelper.GetAsync<List<ApplicationCacheItem>>(key);
            if (applications != null && applications.Count > 0)
            {
                result.AddRange(applications);
            }
        }

        return result;
    }

    public async Task SetRoleApplicationAsync(Guid roleId, List<ApplicationCacheItem> applications)
    {
        var key = $"{CacheKeyConst.Application}{roleId}";
        await CacheHelper.SetAsync(key, applications);
    }

    public async Task RemoveRoleApplicationAsync(Guid roleId)
    {
        await CacheHelper.RemoveAsync($"{CacheKeyConst.Application}{roleId}");
    }

    public async Task<List<MenuCacheItem>> GetRoleMenusAsync(params Guid[] roleIds)
    {
        var result = new List<MenuCacheItem>();

        foreach (var roleId in roleIds)
        {
            var key = $"{CacheKeyConst.Menu}{roleId}";
            var menus = await CacheHelper.GetAsync<List<MenuCacheItem>>(key);
            if (menus != null && menus.Count > 0)
            {
                result.AddRange(menus);
            }
        }

        return result;
    }

    public async Task SetRoleMenuAsync(Guid roleId, List<MenuCacheItem> menus)
    {
        var key = $"{CacheKeyConst.Menu}{roleId}";
        await CacheHelper.SetAsync(key, menus);
    }

    public async Task RemoveRoleMenuAsync(Guid roleId)
    {
        await CacheHelper.RemoveAsync($"{CacheKeyConst.Menu}{roleId}");
    }

    public async Task<List<string>> GetRolePermissionsAsync(params Guid[] roleIds)
    {
        var result = new List<string>();

        foreach (var roleId in roleIds)
        {
            var key = $"{CacheKeyConst.Permission}{roleId}";
            var permissions = await CacheHelper.GetAsync<List<string>>(key);
            if (permissions != null && permissions.Count > 0)
            {
                result.AddRange(permissions);
            }
        }

        return result;
    }

    public async Task SetRolePermissionAsync(Guid roleId, List<string> permissions)
    {
        var key = $"{CacheKeyConst.Permission}{roleId}";
        await CacheHelper.SetAsync(key, permissions);
    }

    public async Task RemoveRolePermissionAsync(Guid roleId)
    {
        await CacheHelper.RemoveAsync($"{CacheKeyConst.Permission}{roleId}");
    }
}
