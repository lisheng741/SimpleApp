using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using Simple.Common.Result;

namespace Simple.Common.Filters;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
public class ApiResultActionFilter : Attribute, IAsyncActionFilter, IOrderedFilter
{
    internal const int FilterOrder = -6000;

    private readonly IApiResultProvider _resultProvider;

    public int Order { get; set; } = FilterOrder;

    public ApiResultActionFilter(IApiResultProvider resultProvider)
    {
        _resultProvider = resultProvider;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var actionContext = await next();

        // 如果已经设置了结果，则直接返回
        if (context.Result != null) return;

        // 如果已有结果，包装成统一返回结果，并返回
        if (actionContext.Result != null)
        {
            actionContext.Result = _resultProvider.ProcessActionResult(actionContext.Result);
            return;
        }

        if (actionContext.Exception is ApiResultException resultException)
        {
            // 如果是结果异常
            actionContext.Result = _resultProvider.ProcessApiResultException(resultException);

            // 标记异常已处理
            actionContext.ExceptionHandled = true;
        }
    }
}
