using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Simple.Common.Authentication.Cookie;

public static class CookieHelper
{
    public static ClaimsIdentity GetClaimsIdentity(CookieModel cookieModel)
    {
        var claims = new List<Claim>()
        {
            new Claim(SimpleClaimTypes.Username, cookieModel.Username),
        };

        foreach (var role in cookieModel.Roles)
        {
            if (string.IsNullOrEmpty(role)) continue;
            claims.Add(new Claim(SimpleClaimTypes.Role, role));
        }

        if(cookieModel.Claims.Count > 0)
        {
            claims.AddRange(cookieModel.Claims);
        }

        var claimsIdentity = new ClaimsIdentity(claims,
            CookieAuthenticationDefaults.AuthenticationScheme,
            SimpleClaimTypes.Username,
            SimpleClaimTypes.Role
        );

        return claimsIdentity;
    }
}
