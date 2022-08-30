using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Simple.Common;

public class SimpleHostedService : IHostedService
{
    public IHost Host { get; set; }

    public SimpleHostedService(IHost host)
    {
        Host = host;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        var serviceProvider = Host.Services;

        // serviceHelper
        ServiceHelper.Configure(serviceProvider);

        // old
        //// Json 配置
        //var jsonSerializerOptions = serviceProvider.GetService<IOptions<JsonSerializerOptions>>();
        //JsonHelper.Configure(jsonSerializerOptions!.Value);

        // new
        // Json 配置
        var jsonOptions = serviceProvider.GetService<IOptions<JsonOptions>>();
        if (jsonOptions != null) JsonHelper.Configure(jsonOptions!.Value.JsonSerializerOptions);

        // AutoMapper
        var mapper = serviceProvider.GetService<IMapper>();
        if (mapper != null) MapperHelper.Configure(mapper);

        // Cache
        var cache = serviceProvider.GetService<IDistributedCache>();
        if (cache != null) CacheHelper.Configure(cache);
        
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
