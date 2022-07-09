using Microsoft.Extensions.Caching.Distributed;

namespace Simple.Common.Components.Cache;

public interface ICacheService : IDistributedCache
{
    Task<TCacheItem?> GetAsync<TCacheItem>(string key, CancellationToken token = default);

    Task SetAsync<TCacheItem>(string key, TCacheItem value, DistributedCacheEntryOptions options, CancellationToken token = default);
}
