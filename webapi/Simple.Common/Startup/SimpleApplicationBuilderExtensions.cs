using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Simple.Common.Middlewares;

namespace Microsoft.AspNetCore.Builder;

public static class SimpleApplicationBuilderExtensions
{
    public static IApplicationBuilder UseSimple(this IApplicationBuilder app)
    {
        app.Use((context, next) =>
        {
            context.Request.EnableBuffering();
            return next(context);
        });

        return app;
    }
}
