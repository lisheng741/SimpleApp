namespace Microsoft.Extensions.Caching.Distributed;

public static class DistributedCacheEextensions
{
    public static async Task<TCacheItem?> GetAsync<TCacheItem>(
            this IDistributedCache cache, 
            string key, 
            CancellationToken token = default)
        where TCacheItem : class
    {
        var cachedBytes = await cache.GetAsync(key, token);
        var value = JsonHelper.Deserialize<TCacheItem>(cachedBytes);
        return value;
    }

    public static async Task SetAsync<TCacheItem>(
            this IDistributedCache cache, 
            string key,
            TCacheItem value,
            DistributedCacheEntryOptions options,
            CancellationToken token = default)
        where TCacheItem : class
    {
        var bytes = JsonHelper.Serialize(value);
        await cache.SetAsync(key, bytes, options, token);
    }
}
