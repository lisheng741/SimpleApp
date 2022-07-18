using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.Options;

namespace Simple.Common.Components.Cache;

public class RedisCacheService : RedisCache, ICacheService
{
    private readonly JsonSerializerOptions _jsonOptions;

    public RedisCacheService(IOptions<RedisCacheOptions> optionsAccessor,
                             IOptions<JsonSerializerOptions> jsonOptionsAccessor)
        : base(optionsAccessor)
    {
        _jsonOptions = jsonOptionsAccessor.Value;
    }

    public virtual async Task<TCacheItem?> GetAsync<TCacheItem>(string key, CancellationToken token = default)
    {
        var cachedBytes = await base.GetAsync(key, token);
        var value = Deserialize<TCacheItem>(cachedBytes);
        return value;
    }

    public virtual async Task SetAsync<TCacheItem>(string key, 
                                                   TCacheItem value, 
                                                   DistributedCacheEntryOptions options, 
                                                   CancellationToken token = default)
    {
        var bytes = Serialize(value);
        await base.SetAsync(key, bytes, options, token);
    }

    protected virtual byte[] Serialize<T>(T obj)
    {
        return Encoding.UTF8.GetBytes(JsonSerializer.Serialize(obj, _jsonOptions));
    }

    protected virtual T? Deserialize<T>(byte[] bytes)
    {
        if (bytes == null)
        {
            return default(T);
        }
        return JsonSerializer.Deserialize<T>(Encoding.UTF8.GetString(bytes), _jsonOptions);
    }
}
