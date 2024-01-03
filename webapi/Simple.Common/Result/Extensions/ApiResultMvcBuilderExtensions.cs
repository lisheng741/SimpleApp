﻿using Microsoft.Extensions.DependencyInjection.Extensions;
using Simple.Common.Filters;
using Simple.Common.Result;

namespace Microsoft.Extensions.DependencyInjection;

public static class ApiResultMvcBuilderExtensions
{
    /// <summary>
    /// 配置 API 统一返回结果
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="filterOrder">结果过滤器排序</param>
    /// <returns></returns>
    public static IMvcBuilder AddApiResult(this IMvcBuilder builder, int filterOrder = ApiResultActionFilter.FilterOrder)
    {
        AddApiResultCore(builder, filterOrder);

        return builder;
    }

    /// <summary>
    /// 配置 API 统一返回结果
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="builder"></param>
    /// <param name="filterOrder">结果过滤器排序</param>
    /// <returns></returns>
    public static IMvcBuilder AddApiResult<T>(this IMvcBuilder builder, int filterOrder = ApiResultActionFilter.FilterOrder)
        where T : IApiResultProvider
    {
        AddApiResult(builder, filterOrder);

        // 替换 IApiResultProvider 的默认实现
        builder.Services.Replace(new ServiceDescriptor(typeof(IApiResultProvider), typeof(T), ServiceLifetime.Singleton));

        return builder;
    }

    internal static void AddApiResultCore(IMvcBuilder builder, int filterOrder)
    {
        // 添加过滤器
        builder.AddMvcOptions(options =>
        {
            // 过滤器管道（先进后出）顺序：Middleware >> ExceptionFilter >> ActionFilter
            // 异常触发顺序：ActionFilter 先于 ExceptionFilter
            // 因为要配合 RequestActionFilter 的使用，所以不能用 ExceptionFilter
            // 先配置 RequestActionFilter(Order=-8000) 再配置 AppResultActionFilter(Order=-6000)
            options.Filters.Add<ApiResultActionFilter>(filterOrder);
        });

        builder.Services.AddSingleton<IApiResultProvider, ApiResultProvider>();
    }
}
