using Microsoft.Extensions.Caching.Distributed;

namespace Simple.Common.Helpers;

public static class CacheHelper
{
    private static IDistributedCache? _cache;

    public static IDistributedCache Cache
    {
        get
        {
            if (_cache == null) throw new NullReferenceException(nameof(Cache));
            return _cache;
        }
    }

    public static void Configure(IDistributedCache? cache)
    {
        if(_cache != null)
        {
            throw new Exception($"{nameof(Cache)}不可修改！");
        }
        _cache = cache ?? throw new ArgumentNullException(nameof(cache));
    }
}
