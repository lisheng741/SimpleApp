using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using Simple.Common.Components.Json;
using Simple.Common.Components.Json.SystemTextJson;

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
        var serializerOptions = new JsonSerializerOptions();

        // 驼峰命名
        serializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;

        // Unicode 编码
        serializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All); 

        // 自定义 Converter
        serializerOptions.Converters.Add(new DateTimeJsonConverter());

        JsonHelper.SerializerOptions = serializerOptions;


        // 注入服务
        services.Configure<JsonSerializerOptions>(options =>
        {
            options.PropertyNamingPolicy = serializerOptions.PropertyNamingPolicy;
            options.Encoder = serializerOptions.Encoder;

            // 自定义 Converter
            foreach(var converter in serializerOptions.Converters)
            {
                options.Converters.Add(converter);
            }
        });


        return services;
    }
}
