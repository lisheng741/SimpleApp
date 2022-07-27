using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Simple.Common.Authentication.Cookie;

namespace Microsoft.AspNetCore.Authentication;

public static class CookieHttpContextExtensions
{
    public static Task SignInAsync(this HttpContext httpContext, CookieModel cookieModel)
    {
        var claimsIdentity = CookieHelper.GetClaimsIdentity(cookieModel);
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

        var authenticationProperties = new AuthenticationProperties()
        {
            IsPersistent = true,
            AllowRefresh = true,
        };

        return httpContext.SignInAsync(claimsPrincipal, authenticationProperties);
    }
}
