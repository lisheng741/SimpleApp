using Microsoft.AspNetCore.Mvc;

namespace Simple.Common;

public class AppResultOptions
{
    public Func<AppResultException, IActionResult> _resultFactory = default!;

    public Func<AppResultException, IActionResult> ResultFactory
    {
        get => _resultFactory;
        set => _resultFactory = value ?? throw new ArgumentNullException(nameof(value));
    }
}
