using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;

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

        //// Api 结果 弃用
        //result = new ContentResult()
        //{
        //    StatusCode = resultException.AppResult.Code,
        //    ContentType = "application/json",
        //    Content = JsonHelper.Serialize(resultException.AppResult)
        //};

        // Api 结果 统一处理为 ObjectResult 与控制器保持一致
        result = new ObjectResult(resultException.AppResult)
        {
            StatusCode = resultException.AppResult.Code
        };

        return result;
    }
}
