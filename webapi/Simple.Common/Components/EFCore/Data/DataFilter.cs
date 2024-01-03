using System.Collections.Concurrent;
using Simple.Common.Core;

namespace Simple.Common.EFCore.Data;

public class DataFilter
{
    private readonly ConcurrentDictionary<Type, object> _filters;

    public DataFilter()
    {
        _filters = new ConcurrentDictionary<Type, object>();
    }

    public IDisposable Enable<TFilter>()
        where TFilter : class
    {
        return GetFilter<TFilter>().Enable();
    }

    public IDisposable Disable<TFilter>()
        where TFilter : class
    {
        return GetFilter<TFilter>().Disable();
    }

    public bool IsEnabled<TFilter>()
        where TFilter : class
    {
        return GetFilter<TFilter>().IsEnabled;
    }

    private DataFilter<TFilter> GetFilter<TFilter>()
        where TFilter : class
    {
        var filter = _filters.GetOrAdd(typeof(TFilter), new DataFilter<TFilter>()) as DataFilter<TFilter>;
        return filter!;
    }
}

public class DataFilter<TFilter>
    where TFilter : class
{
    public virtual bool IsEnabled { get; protected set; }

    public DataFilter(bool isEnabled = true)
    {
        IsEnabled = isEnabled;
    }

    public virtual IDisposable Enable()
    {
        if (IsEnabled)
        {
            return NullDisposable.Instance;
        }

        IsEnabled = true;

        return new ActionDisposable(() => Disable());
    }

    public virtual IDisposable Disable()
    {
        if (!IsEnabled)
        {
            return NullDisposable.Instance;
        }

        IsEnabled = false;

        return new ActionDisposable(() => Enable());
    }
}
