using Microsoft.AspNetCore.Http;

namespace Simple.Common.Services;

public interface ISimpleService
{
    IServiceProvider ServiceProvider { get; }

    HttpContext HttpContext { get; }

    TService GetService<TService>() where TService : class;

    object GetService(Type type);
}

public class SimpleService : ISimpleService
{
    protected IDictionary<Type, object> CacheServices { get; set; } = new Dictionary<Type, object>();

    public SimpleService(IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;

        // 不要在构造函数中捕获 IHttpContextAccessor.HttpContext。
        // 详见：https://docs.microsoft.com/zh-cn/aspnet/core/fundamentals/http-context?view=aspnetcore-6.0#httpcontext-isnt-thread-safe
        //HttpContext = accessor.HttpContext ?? throw new ArgumentNullException(nameof(HttpContext));
    }

    public virtual IServiceProvider ServiceProvider { get; private set; }

    public virtual IHttpContextAccessor HttpContextAccessor => GetService<IHttpContextAccessor>();

    public virtual HttpContext HttpContext => HttpContextAccessor.HttpContext!;

    public virtual TService GetService<TService>()
        where TService : class
    {
        var service = GetService(typeof(TService)) as TService;
        if (service == null)
        {
            throw new NullReferenceException(nameof(service));
        }
        return service;
    }

    public virtual object GetService(Type type)
    {
        if (CacheServices.TryGetValue(type, out var obj))
        {
            return obj;
        }

        var service = ServiceProvider.GetService(type);
        if (service == null)
        {
            throw new NullReferenceException(nameof(service));
        }
        return CacheServices[type] = service;
    }
}
