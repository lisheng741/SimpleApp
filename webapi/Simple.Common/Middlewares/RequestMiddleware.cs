using Microsoft.AspNetCore.Http;
using Simple.Common.EventBus;

namespace Simple.Common.Middlewares;

public class RequestMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IEventPublisher _publisher;

    public RequestMiddleware(RequestDelegate next, IEventPublisher publisher)
    {
        _next = next;
        _publisher = publisher;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var request = context.Request;
        var response = context.Response;

        request.EnableBuffering();

        request.Body.Seek(0, SeekOrigin.Begin);
        var requestBodyReader = new StreamReader(request.Body, Encoding.UTF8);
        string requestBody = await requestBodyReader.ReadToEndAsync();
        request.Body.Seek(0, SeekOrigin.Begin);

        using var ms = new MemoryStream();
        response.Body = ms;

        await _next(context);

        ms.Seek(0, SeekOrigin.Begin);
        var responseBodyReader = new StreamReader(ms, Encoding.UTF8);
        string responseBody = await responseBodyReader.ReadToEndAsync();
        ms.Seek(0, SeekOrigin.Begin);
    }
}
