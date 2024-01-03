namespace Simple.Common.Core;

public class ActionDisposable : IDisposable
{
    private readonly Action _action;

    public ActionDisposable(Action action)
    {
        if (action == null) throw new ArgumentNullException(nameof(action));

        _action = action;
    }

    public void Dispose()
    {
        _action();
    }
}
