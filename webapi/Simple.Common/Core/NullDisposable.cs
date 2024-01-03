namespace Simple.Common.Core;

public class NullDisposable : IDisposable
{
    public static NullDisposable Instance { get; } = new NullDisposable();

    private NullDisposable()
    {
    }

    public void Dispose()
    {
    }
}
