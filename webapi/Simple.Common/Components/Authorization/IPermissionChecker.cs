using System.Security.Claims;

namespace Simple.Common.Components.Authorization;

public interface IPermissionChecker
{
    Task<bool> IsGrantedAsync(ClaimsPrincipal claimsPrincipal, string name);
}
