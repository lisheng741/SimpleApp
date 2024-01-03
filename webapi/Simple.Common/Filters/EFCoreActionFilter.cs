using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace Simple.Common.Filters;

internal class EFCoreActionFilter : IAsyncActionFilter, IOrderedFilter
{
    internal const int FilterOrder = 2000;

    public int Order => FilterOrder;

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var actionContext = await next();

        // 如果已经设置了结果，则直接返回
        if (context.Result != null) return;

        // 如果已有结果，包装成统一返回结果，并返回
        if (actionContext.Result != null) return;

        if (actionContext.Exception is DbUpdateConcurrencyException concurrencyException)
        {
            // 处理为统一返回结果
            var apiResult = ApiResultHelper.Result409Conflict("数据已被其他人操作，你的操作没有生效！");
            actionContext.Result = new ObjectResult(apiResult)
            {
                StatusCode = apiResult.Code
            };

            // 标记异常已处理
            actionContext.ExceptionHandled = true;
        }
        else if (actionContext.Exception is DbUpdateException updateException)
        {
            // 处理为统一返回结果
            var apiResult = ApiResultHelper.Result409Conflict("数据库写入错误！");
            actionContext.Result = new ObjectResult(apiResult)
            {
                StatusCode = apiResult.Code
            };

            // 标记异常已处理
            actionContext.ExceptionHandled = true;
        }
    }
}
