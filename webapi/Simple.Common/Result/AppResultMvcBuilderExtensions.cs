using Microsoft.AspNetCore.Mvc;
using Simple.Common.Filters;

namespace Microsoft.Extensions.DependencyInjection;

public static class AppResultMvcBuilderExtensions
{
    public static IMvcBuilder AddAppResult(this IMvcBuilder builder, Action<AppResultOptions>? setupAction = null)
    {
        builder.AddMvcOptions(options =>
        {
            options.Filters.Add<AppResultExceptionFilter>();
        });

        builder.Services.Configure<AppResultOptions>(options =>
        {
            // 默认结果工厂（!Problem: 目前只能处理 Api 结果）
            options.ResultFactory = resultException =>
            {
                IActionResult result;

                // Mvc 结果

                // Api 结果
                result = new ContentResult()
                {
                    StatusCode = resultException.AppResult.Code,
                    ContentType = "application/json",
                    Content = JsonHelper.Serialize(resultException.AppResult)
                };

                return result;
            };
        });

        if(setupAction != null) builder.Services.Configure(setupAction);

        return builder;
    }
}
