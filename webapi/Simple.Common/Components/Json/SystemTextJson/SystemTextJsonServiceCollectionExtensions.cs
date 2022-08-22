using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using Microsoft.AspNetCore.Mvc;
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
        // 不对 JsonSerializerOptions 进行配置，直接读取 Microsoft.AspNetCore.Mvc.JsonOptions 的 Json 配置
        //// 配置 JsonSerializerOptions
        //services.Configure<JsonSerializerOptions>(options =>
        //{
        //    ConfigureJsonOptions(options);
        //});

        // 配置 JsonOptions
        services.Configure<JsonOptions>(options =>
        {
            ConfigureJsonOptions(options.JsonSerializerOptions);
        });

        return services;

        // 内部函数 配置 JsonSerializerOptions
        JsonSerializerOptions ConfigureJsonOptions(JsonSerializerOptions options)
        {
            // 驼峰命名
            options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;

            // Unicode 编码
            options.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);

            // 忽略循环引用
            // https://docs.microsoft.com/zh-cn/dotnet/standard/serialization/system-text-json-preserve-references
            options.ReferenceHandler = ReferenceHandler.IgnoreCycles;

            // 自定义 Converter
            options.Converters.Add(new DateTimeJsonConverter());
            options.Converters.Add(new EnumJsonConverter());

            // 如果传入自定义配置
            if (setupAction != null) setupAction(options);

            return options;
        }
    }
}
