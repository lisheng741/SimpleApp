using System.Text.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;

namespace Simple.Common;

public static class SimpleStartup
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    public static void ConfigureServices(IServiceCollection services)
    {

    }

    /// <summary>
    /// 初始化配置，主要是获取必要的 IOptions 给静态类配置
    /// </summary>
    /// <param name="app"></param>
    public static void Configure(IApplicationBuilder app)
    {
        // Json 配置
        var jsonSerializerOptions = app.ApplicationServices.GetService<IOptions<JsonSerializerOptions>>();
        JsonHelper.Configure(jsonSerializerOptions!.Value);
    }
}
