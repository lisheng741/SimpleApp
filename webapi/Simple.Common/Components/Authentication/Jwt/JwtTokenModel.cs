using System.Security.Claims;

namespace Simple.Common.Authentication.Jwt;

public class JwtTokenModel
{
    public string Username { get; private set; }
    public string[] Roles { get; private set; }
    public List<Claim> Claims { get; set; } = new List<Claim>();
    public int Expiration { get; set; } = 1800;

    public JwtTokenModel(string username, params string[] roles)
    {
        Username = username;
        Roles = roles;
    }

    public JwtTokenModel(string username, List<Claim> claims, params string[] roles)
        : this(username, roles)
    {
        Claims = claims;
    }

    public JwtTokenModel(string username, List<Claim> claims, int expiration, params string[] roles)
        : this(username, claims, roles)
    {
        Expiration = expiration;
    }
}
