using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using UAParser;

namespace Simple.Common.Filters;

public class RequestActionFilter : IAsyncActionFilter
{
    private readonly IEventPublisher _publisher;
    private readonly ICurrentUserService _currentUser;

    public RequestActionFilter(IEventPublisher publisher, ICurrentUserService currentUser)
    {
        _publisher = publisher;
        _currentUser = currentUser;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var httpContext = context.HttpContext;
        var request = context.HttpContext.Request;
        var headers = request.Headers;
        var actionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;

        //request.EnableBuffering(); // 允许多次读取 Body
        request.Body.Position = 0;
        var reader = new StreamReader(request.Body, Encoding.UTF8);
        string body = await reader.ReadToEndAsync();
        request.Body.Seek(0, SeekOrigin.Begin);
        //request.Body.Position = 0;

        var sw = new Stopwatch();
        sw.Start();
        var actionContext = await next();
        sw.Stop();

        bool isSuccess = actionContext.Exception == null; // 没有异常即认为请求成功
        var clientInfo = headers.ContainsKey("User-Agent") ? Parser.GetDefault().Parse(headers["User-Agent"]) : null;
        string name = actionDescriptor == null ? "" : actionDescriptor.MethodInfo.GetSummary();

        string result = "";
        string message = "";
        if(actionContext.Exception is AppResultException appResultException)
        {
            // 如果是 AppResultException 说明是统一返回
            result = JsonHelper.Serialize(appResultException.AppResult);
            message = appResultException.AppResult.Message ?? "";
        }
        else if (actionContext.Result is ObjectResult objectResult)
        {
            // 正常接口都是 ObjectResult
            result = JsonHelper.Serialize(objectResult.Value);
            if(objectResult.Value is AppResult appResult)
            {
                message = appResult.Message ?? "";
            }
        }
        else if (actionContext.Result is JsonResult jsonResult)
        {
            result = JsonHelper.Serialize(jsonResult.Value);
        }

        var @event = new RequestEvent()
        {
            Name = name,
            Message = message,
            Account = _currentUser.UserName,
            IsSuccess = isSuccess,
            Browser = clientInfo?.UA.Family + clientInfo?.UA.Major,
            OperatingSystem = clientInfo?.OS.Family + clientInfo?.OS.Major,
            Ip = httpContext.GetRequestIPv4(),
            Url = request.GetRequestUrlAddress(),
            Path = request.Path,
            ClassName = context.Controller.ToString(),
            MethodName = actionDescriptor?.ActionName,
            RequestMethod = request.Method,
            //Parameter = context.ActionArguments.Count < 1 ? string.Empty : JsonHelper.Serialize(context.ActionArguments),
            Body = body,
            Result = result,
            ElapsedTime = sw.ElapsedMilliseconds,
            OperatingTime = DateTimeOffset.Now,
        };

        await _publisher.PublishAsync(@event);
    }
}
