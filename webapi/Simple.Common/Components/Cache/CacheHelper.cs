using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.Extensions.Caching.Distributed;

namespace Simple.Common.Helpers;

public static class CacheHelper
{
    private static IDistributedCache? _cache;

    private static IDistributedCache Cache
    {
        get
        {
            if (_cache == null) throw new NullReferenceException(nameof(Cache));
            return _cache;
        }
    }

    /// <summary>
    /// 缓存所有索引键（Key）
    /// </summary>
    public const string KeySetCacheKey = "key_set"; 

    public static void Configure(IDistributedCache? cache)
    {
        if(_cache != null)
        {
            throw new Exception($"{nameof(Cache)}不可修改！");
        }
        _cache = cache ?? throw new ArgumentNullException(nameof(cache));
    }

    #region 缓存操作方法

    public static byte[] Get(string key)
    {
        return Cache.Get(key);
    }

    public static Task<byte[]> GetAsync(string key, CancellationToken token = default)
    {
        return Cache.GetAsync(key, token);
    }

    public static Task<TCacheItem?> GetAsync<TCacheItem>(string key, CancellationToken token = default)
        where TCacheItem : class
    {
        return Cache.GetAsync<TCacheItem>(key, token);
    }

    public static void Refresh(string key)
    {
        Cache.Refresh(key);
    }

    public static Task RefreshAsync(string key, CancellationToken token = default)
    {
        return Cache.RefreshAsync(key, token);
    }

    public static void Remove(string key)
    {
        Cache.Remove(key);

        RemoveCacheKey(key);
    }

    public static async Task RemoveAsync(string key, CancellationToken token = default)
    {
        await Cache.RemoveAsync(key, token);

        await RemoveCacheKeyAsync(key);
    }

    public static void RemoveRange(IEnumerable<string> keys)
    {
        foreach(string key in keys)
        {
            Cache.Remove(key);
        }

        RemoveRangeCacheKey(keys);
    }

    public static async Task RemoveRangeAsync(IEnumerable<string> keys, CancellationToken token = default)
    {
        foreach (string key in keys)
        {
            await Cache.RemoveAsync(key, token);
        }

        await RemoveRangeCacheKeyAsync(keys, token);
    }

    public static void Set(string key, byte[] value)
    {
        Cache.Set(key, value);

        AddCacheKey(key);
    }

    public static void Set(string key, byte[] value, DistributedCacheEntryOptions options)
    {
        Cache.Set(key, value, options);

        AddCacheKey(key);
    }

    public static async Task SetAsync(string key, byte[] value, CancellationToken token = default)
    {
        await Cache.SetAsync(key, value, token);

        await AddCacheKeyAsync(key);
    }

    public static async Task SetAsync(string key, byte[] value, DistributedCacheEntryOptions options, CancellationToken token = default)
    {
        await Cache.SetAsync(key, value, options, token);

        await AddCacheKeyAsync(key);
    }

    public static async Task SetAsync<TCacheItem>(string key, TCacheItem value, CancellationToken token = default)
        where TCacheItem : class
    {
        await Cache.SetAsync<TCacheItem>(key, value, token);

        await AddCacheKeyAsync(key);
    }

    public static async Task SetAsync<TCacheItem>(string key, TCacheItem value, DistributedCacheEntryOptions options, CancellationToken token = default)
        where TCacheItem : class
    {
        await Cache.SetAsync<TCacheItem>(key, value, options, token);

        await AddCacheKeyAsync(key);
    }

    #endregion


    #region 索引键操作方法

    public static HashSet<string> GetKeySet()
    {
        var keySet = Cache.Get<HashSet<string>>(KeySetCacheKey);
        return keySet ?? new HashSet<string>();
    }

    public static async Task<HashSet<string>> GetKeySetAsync()
    {
        var keySet = await Cache.GetAsync<HashSet<string>>(KeySetCacheKey);
        return keySet ?? new HashSet<string>();
    }

    public static void AddCacheKey(string key)
    {
        // 获取缓存键集合
        var keySet = GetKeySet();

        // 成功添加，则写入新的缓存
        if (keySet.Add(key))
        {
            Cache.Set(KeySetCacheKey, keySet);
        }
    }

    public static async Task AddCacheKeyAsync(string key, CancellationToken token = default)
    {
        // 获取缓存键集合
        var keySet = await GetKeySetAsync();

        // 成功添加，则写入新的缓存
        if (keySet.Add(key))
        {
            await Cache.SetAsync(KeySetCacheKey, keySet, token);
        }
    }

    public static void AddRangeCacheKey(IEnumerable<string> keys)
    {
        // 获取缓存键集合
        var keySet = GetKeySet();

        // 添加
        foreach (var key in keys)
        {
            keySet.Add(key);
        }
        // 写入缓存
        Cache.Set(KeySetCacheKey, keySet);
    }

    public static async Task AddRangeCacheKeyAsync(IEnumerable<string> keys, CancellationToken token = default)
    {
        // 获取缓存键集合
        var keySet = await GetKeySetAsync();

        // 添加
        foreach (var key in keys)
        {
            keySet.Add(key);
        }
        // 写入缓存
        await Cache.SetAsync(KeySetCacheKey, keySet, token);
    }

    public static void RemoveCacheKey(string key)
    {
        // 读缓存
        var keySet = GetKeySet();

        // 如果缓存存在，且成功删除，则更新缓存
        if (keySet.Remove(key))
        {
            Cache.Set(KeySetCacheKey, keySet);
        }
    }

    public static async Task RemoveCacheKeyAsync(string key, CancellationToken token = default)
    {
        // 读缓存
        var keySet = await GetKeySetAsync();

        // 如果缓存存在，且成功删除，则更新缓存
        if (keySet.Remove(key))
        {
            await Cache.SetAsync(KeySetCacheKey, keySet, token);
        }
    }

    public static void RemoveRangeCacheKey(IEnumerable<string> keys)
    {
        // 读缓存
        var keySet = GetKeySet();

        foreach (string key in keys)
        {
            keySet.Remove(key);
        }
        Cache.Set(KeySetCacheKey, keySet);
    }

    public static async Task RemoveRangeCacheKeyAsync(IEnumerable<string> keys, CancellationToken token = default)
    {
        // 读缓存
        var keySet = await GetKeySetAsync();

        foreach (string key in keys)
        {
            keySet.Remove(key);
        }
        await Cache.SetAsync(KeySetCacheKey, keySet, token);
    }

    #endregion


    #region 辅助方法

    public static string GetKey(params string[] partialKeys)
    {
        return string.Join("", partialKeys);
    }

    #endregion
}
