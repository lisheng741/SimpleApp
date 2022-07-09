using System.Security.Claims;

namespace Simple.Common.Components.Authentication.Cookie;

public class CookieModel
{
    public string Username { get; private set; }
    public string[] Roles { get; private set; }
    public List<Claim> Claims { get; set; }

    public CookieModel(string username, params string[] roles)
    {
        Username = username;
        Roles = roles;
        Claims = new List<Claim>();
    }

    public CookieModel(string username, List<Claim> claims, params string[] roles)
        : this(username, roles)
    {
        Claims = claims;
    }
}
