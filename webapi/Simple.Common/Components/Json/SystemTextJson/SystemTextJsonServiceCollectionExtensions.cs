using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using Simple.Common.Json;
using Simple.Common.Json.SystemTextJson;

namespace Microsoft.Extensions.DependencyInjection;

public static class SystemTextJsonServiceCollectionExtensions
{
    /// <summary>
    /// 添加默认 Json 序列化/反序列化 配置
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddSimpleJsonOptions(this IServiceCollection services, Action<JsonSerializerOptions>? setupAction = null)
    {
        // 注入服务
        services.Configure<JsonSerializerOptions>(options =>
        {
            // 驼峰命名
            options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;

            // Unicode 编码
            options.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);

            // 自定义 Converter
            options.Converters.Add(new DateTimeJsonConverter());
        });

        // 如果传入自定义配置
        if(setupAction != null) services.Configure(setupAction);

        return services;
    }
}
