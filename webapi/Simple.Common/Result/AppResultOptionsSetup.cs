using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Simple.Common;

internal class AppResultOptionsSetup : IConfigureOptions<AppResultOptions>
{
    public void Configure(AppResultOptions options)
    {
        // 默认结果工厂
        options.ResultFactory = resultException =>
        {
            return GetResult(resultException);
        };
    }

    internal static IActionResult GetResult(AppResultException resultException)
    {
        IActionResult result;

        //（!Problem: 目前只能处理 Api 结果）

        // Mvc 结果

        // Api 结果
        result = new ContentResult()
        {
            StatusCode = resultException.AppResult.Code,
            ContentType = "application/json",
            Content = JsonHelper.Serialize(resultException.AppResult)
        };

        return result;
    }
}
