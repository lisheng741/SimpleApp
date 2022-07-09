using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Simple.Common.Components.Cache;

namespace Simple.Services;

public class TestChildService : TestService
{
    private readonly ICacheService _cacheService;

    public TestChildService(ICacheService cacheService)
    {
        _cacheService = cacheService;
    }

    public async Task Test()
    {
        await _cacheService.SetAsync("test", 
                new SysRole() { Id = Guid.NewGuid(), Name = "TestRole" },
                new DistributedCacheEntryOptions()
            );

        var role = await _cacheService.GetAsync<SysRole>("test");
    }
}
