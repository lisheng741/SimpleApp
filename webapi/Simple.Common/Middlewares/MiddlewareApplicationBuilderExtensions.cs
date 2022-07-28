using Simple.Common.Middlewares;

namespace Microsoft.AspNetCore.Builder;

public static class MiddlewareApplicationBuilderExtensions
{
    /// <summary>
    /// Api 异常处理中间件
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseApiException(this IApplicationBuilder app)
    {
        return app.UseMiddleware<ApiExceptionMiddleware>();
    }
}
