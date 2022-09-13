using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Simple.Common.Filters;

namespace Microsoft.Extensions.DependencyInjection;

public static class AppResultMvcBuilderExtensions
{
    public static IMvcBuilder AddAppResult(this IMvcBuilder builder, Action<AppResultOptions>? setupAction = null)
    {
        // 添加过滤器
        builder.AddMvcOptions(options =>
        {
            // 过滤器管道（先进后出）顺序：Middleware >> ExceptionFilter >> ActionFilter
            // 异常触发顺序：ActionFilter >> ExceptionFilter

            // ExceptionFilter 实现，Order = -6000，这个过滤器实际上没有用
            //options.Filters.Add<AppResultExceptionFilter>();

            // ActionFilter 实现，Order = -6000，这个过滤器会标记 AppResultException 被处理，故而 ExceptionFilter 将无法捕捉到
            options.Filters.Add<AppResultActionFilter>();
        });

        // 默认 AppResultOptions 配置
        builder.Services.AddTransient<IConfigureOptions<AppResultOptions>, AppResultOptionsSetup>();

        // 如果有自定义配置
        if(setupAction != null) builder.Services.Configure(setupAction);

        return builder;
    }
}
