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
            options.Filters.Add<AppResultExceptionFilter>();
        });

        // 默认 AppResultOptions 配置
        builder.Services.AddTransient<IConfigureOptions<AppResultOptions>, AppResultOptionsSetup>();

        // 如果有自定义配置
        if(setupAction != null) builder.Services.Configure(setupAction);

        return builder;
    }
}
