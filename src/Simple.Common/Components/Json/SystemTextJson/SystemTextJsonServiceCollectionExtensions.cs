using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace Microsoft.Extensions.DependencyInjection;

public static class SystemTextJsonServiceCollectionExtensions
{
    /// <summary>
    /// 添加 Json 配置
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddJsonOptions(this IServiceCollection services)
    {
        services.Configure<JsonSerializerOptions>(config =>
        {
            config.PropertyNamingPolicy = JsonNamingPolicy.CamelCase; // 驼峰命名
            config.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All); // Unicode 编码

            // 自定义 Converter
            config.Converters.AddDateTimeJsonConverter();
        });

        return services;
    }
}
