using Microsoft.AspNetCore.Mvc;
using Simple.Common.Filters;

namespace Microsoft.Extensions.DependencyInjection;

public static class DataValidationMvcBuilderExtensions
{
    /// <summary>
    /// 配置模型验证
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static IMvcBuilder AddDataValidation(this IMvcBuilder builder)
    {
        builder.ConfigureApiBehaviorOptions(options =>
        {
            // 禁用默认模型验证过滤器
            options.SuppressModelStateInvalidFilter = true;
        });

        builder.AddMvcOptions(options =>
        {
            // 添加自定义模型验证过滤器
            options.Filters.Add<DataValidationFilter>();
        });

        return builder;
    }

    //// 配置模型验证
    //public static IServiceCollection AddDataValidation(this IServiceCollection services)
    //{
    //    services.Configure<ApiBehaviorOptions>(options =>
    //    {
    //        // 禁用默认模型验证过滤器
    //        options.SuppressModelStateInvalidFilter = true;
    //    });

    //    services.Configure<MvcOptions>(options =>
    //    {
    //        // 添加自定义模型验证过滤器
    //        options.Filters.Add<DataValidationFilter>();
    //    });

    //    return services;
    //}
}
