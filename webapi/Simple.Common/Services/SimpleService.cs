using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Simple.Common.Services;

public interface ISimpleService
{
    public HttpContext HttpContext { get; }
}

public class SimpleService : ISimpleService
{
    private readonly IHttpContextAccessor _accessor;

    public SimpleService(IHttpContextAccessor accessor)
    {
        _accessor = accessor;

        // 不要在构造函数中捕获 IHttpContextAccessor.HttpContext。
        // 详见：https://docs.microsoft.com/zh-cn/aspnet/core/fundamentals/http-context?view=aspnetcore-6.0#httpcontext-isnt-thread-safe
        //HttpContext = accessor.HttpContext ?? throw new ArgumentNullException(nameof(HttpContext));
    }

    public HttpContext HttpContext => _accessor.HttpContext!;
}
