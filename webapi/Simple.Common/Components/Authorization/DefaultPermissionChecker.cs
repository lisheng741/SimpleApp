using System.Security.Claims;

namespace Simple.Common.Authorization;

public class DefaultPermissionChecker : IPermissionChecker
{
    public virtual Task<bool> IsGrantedAsync(ClaimsPrincipal claimsPrincipal, string name)
    {
        return Task.FromResult(true);
    }
}
