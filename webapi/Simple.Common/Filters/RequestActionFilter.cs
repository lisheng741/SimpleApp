using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using UAParser;

namespace Simple.Common.Filters;

/// <summary>
/// 禁用请求记录过滤器
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class DisabledRequestRecordAttribute : Attribute { }

public class RequestActionFilter : IAsyncActionFilter, IOrderedFilter
{
    internal const int FilterOrder = -8000;

    private readonly IEventPublisher _publisher;
    private readonly ICurrentUserService _currentUser;

    public int Order => FilterOrder;

    public RequestActionFilter(IEventPublisher publisher, ICurrentUserService currentUser)
    {
        _publisher = publisher;
        _currentUser = currentUser;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        bool isSkipRecord = false;
        var httpContext = context.HttpContext;
        var request = context.HttpContext.Request;
        var headers = request.Headers;
        var actionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;

        // 判断是否需要跳过
        if (!AppSettings.RecordRequest.IsEnabled) isSkipRecord = true;
        if (actionDescriptor == null) isSkipRecord = true;
        if (AppSettings.RecordRequest.IsSkipGetMethod && request.Method.ToUpper() == "GET") isSkipRecord = true;

        foreach (var metadata in actionDescriptor!.EndpointMetadata)
        {
            if (metadata is DisabledRequestRecordAttribute)
            {
                isSkipRecord = true;
            }
        }

        // 进入管道的下一个过滤器，并跳过剩下动作
        if (isSkipRecord)
        {
            await next();
            return;
        }

        //request.Body.Position = 0;
        request.Body.Seek(0, SeekOrigin.Begin);
        var reader = new StreamReader(request.Body, Encoding.UTF8);
        string body = await reader.ReadToEndAsync();
        request.Body.Seek(0, SeekOrigin.Begin);

        var sw = new Stopwatch();
        sw.Start();
        var actionContext = await next();
        sw.Stop();

        bool isSuccess = actionContext.Exception == null; // 没有异常即认为请求成功
        var clientInfo = headers.ContainsKey("User-Agent") ? Parser.GetDefault().Parse(headers["User-Agent"]) : null;
        string name = actionDescriptor == null ? "" : actionDescriptor.MethodInfo.GetSummary();

        string result = "";
        string message = "";

        // 正常 API 都是 ObjectResult 目前只处理 ObjectResult
        if (actionContext.Result is ObjectResult objectResult)
        {
            result = JsonHelper.Serialize(objectResult.Value);
            if (objectResult.Value is ApiResult apiResult)
            {
                isSuccess = apiResult.Success;
                message = apiResult.Message ?? "";
            }
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
