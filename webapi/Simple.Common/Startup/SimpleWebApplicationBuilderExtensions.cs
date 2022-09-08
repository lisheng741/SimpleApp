using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using NLog.Web;
using Simple.Common.Filters;
using Simple.Common.Middlewares;

namespace Microsoft.Extensions.DependencyInjection;

public static class SimpleWebApplicationBuilderExtensions
{
    public static WebApplicationBuilder SimpleConfigure(this WebApplicationBuilder builder)
    {
        // 配置
        var configuration = builder.Configuration;
        AppSettings.Configure(configuration);

        // 日志
        //builder.Logging.ClearProviders(); // .AddConsole()
        builder.Host.UseNLog();

        // 添加 HostedService 对静态 Helper 进行配置
        builder.Services.AddHostedService<SimpleHostedService>();

        // 默认事件总线
        builder.Services.AddEventBusDefault();

        // 添加过滤器
        builder.Services.Configure<MvcOptions>(options =>
        {
            options.Filters.Add<RequestActionFilter>();
        });

        return builder;
    }
}
