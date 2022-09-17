using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Simple.Common.Middlewares;

namespace Microsoft.AspNetCore.Builder;

public static class SimpleApplicationBuilderExtensions
{
    public static IApplicationBuilder UseSimplePipeline(this IApplicationBuilder app)
    {
        app.Use((context, next) =>
        {
            // 确保 Body 可以多次读取。通常，在内存中缓冲请求正文;将大于 30K 字节的请求写入磁盘。
            context.Request.EnableBuffering();
            return next(context);
        });

        // 全局异常处理
        app.UseApiException();

        return app;
    }
}
