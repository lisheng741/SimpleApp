namespace Microsoft.Extensions.Caching.Distributed;

public static class DistributedCacheEextensions
{
    public static TCacheItem? Get<TCacheItem>(this IDistributedCache cache, string key)
        where TCacheItem : class
    {
        var cachedBytes = cache.Get(key);
        var value = JsonHelper.Deserialize<TCacheItem>(cachedBytes);
        return value;
    }

    public static async Task<TCacheItem?> GetAsync<TCacheItem>(this IDistributedCache cache, string key, CancellationToken token = default)
        where TCacheItem : class
    {
        var cachedBytes = await cache.GetAsync(key, token);
        var value = JsonHelper.Deserialize<TCacheItem>(cachedBytes);
        return value;
    }

    public static void Set<TCacheItem>(this IDistributedCache cache, string key, TCacheItem value)
        where TCacheItem : class
    {
        var bytes = JsonHelper.SerializeToUtf8Bytes(value);
        cache.Set(key, bytes);
    }

    public static void Set<TCacheItem>(this IDistributedCache cache, string key, TCacheItem value, DistributedCacheEntryOptions options)
        where TCacheItem : class
    {
        var bytes = JsonHelper.SerializeToUtf8Bytes(value);
        cache.Set(key, bytes, options);
    }

    public static async Task SetAsync<TCacheItem>(
            this IDistributedCache cache,
            string key,
            TCacheItem value,
            CancellationToken token = default)
        where TCacheItem : class
    {
        var bytes = JsonHelper.SerializeToUtf8Bytes(value);
        await cache.SetAsync(key, bytes, token);
    }

    public static async Task SetAsync<TCacheItem>(
            this IDistributedCache cache,
            string key,
            TCacheItem value,
            DistributedCacheEntryOptions options,
            CancellationToken token = default)
        where TCacheItem : class
    {
        var bytes = JsonHelper.SerializeToUtf8Bytes(value);
        await cache.SetAsync(key, bytes, options, token);
    }
}
