using System.Security.Claims;

namespace Simple.Common.Authentication.Cookie;

public class CookieModel
{
    public string UserName { get; private set; }
    public string[] Roles { get; private set; }
    public List<Claim> Claims { get; set; }

    public CookieModel(string userName, params string[] roles)
    {
        UserName = userName;
        Roles = roles;
        Claims = new List<Claim>();
    }

    public CookieModel(string userName, List<Claim> claims, params string[] roles)
        : this(userName, roles)
    {
        Claims = claims;
    }
}
