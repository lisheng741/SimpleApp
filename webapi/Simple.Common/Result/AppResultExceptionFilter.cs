using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;

namespace Simple.Common.Filters;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
public class AppResultExceptionFilter : Attribute, IAsyncExceptionFilter, IExceptionFilter, IOrderedFilter
{
    private readonly AppResultOptions _options;

    public int Order { get; set; } = -6000;

    public AppResultExceptionFilter(IOptions<AppResultOptions> options)
    {
        _options = options.Value;
    }

    public virtual void OnException(ExceptionContext context)
    {
        // 如果其他过滤器已经设置了结果，则直接返回
        if (context.Result != null) return;

        // 如果异常已经被处理了，则直接返回
        if (context.ExceptionHandled) return;

        // 如果不是结果异常，则直接返回
        if (!(context.Exception is AppResultException resultException)) return;

        // 如果是结果异常，处理成返回结果，并标记异常已处理
        context.Result = _options.ResultFactory(resultException);
        context.ExceptionHandled = true;
    }

    public virtual Task OnExceptionAsync(ExceptionContext context)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        OnException(context);
        return Task.CompletedTask;
    }
}
