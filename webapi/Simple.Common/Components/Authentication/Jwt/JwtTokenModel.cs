using System.Security.Claims;

namespace Simple.Common.Authentication.Jwt;

public class JwtTokenModel
{
    public string UserName { get; private set; }
    public string[] Roles { get; private set; }
    public List<Claim> Claims { get; set; } = new List<Claim>();
    public int Expiration { get; set; } = 1800;

    public JwtTokenModel(string userName, params string[] roles)
    {
        UserName = userName;
        Roles = roles;
    }

    public JwtTokenModel(string userName, List<Claim> claims, params string[] roles)
        : this(userName, roles)
    {
        Claims = claims;
    }

    public JwtTokenModel(string userName, List<Claim> claims, int expiration, params string[] roles)
        : this(userName, claims, roles)
    {
        Expiration = expiration;
    }
}
