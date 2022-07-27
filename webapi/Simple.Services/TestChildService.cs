using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Simple.Common.Services;

namespace Simple.Services;

public class TestChildService : TestService
{
    private readonly IDistributedCache _cache;

    public TestChildService(IDistributedCache cache, ICurrentUserService currentUser)
        : base(currentUser)
    {
        _cache = cache;
    }

    public async Task Test()
    {
        await _cache.SetAsync("test", 
                new SysRole() { Id = Guid.NewGuid(), Name = "TestRole" },
                new DistributedCacheEntryOptions()
            );

        var role = await _cache.GetAsync<SysRole>("test");
    }
}
