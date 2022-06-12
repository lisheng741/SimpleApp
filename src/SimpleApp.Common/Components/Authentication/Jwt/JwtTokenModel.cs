namespace SimpleApp.Common.Components.Authentication.Jwt;

public class JwtTokenModel
{
    public string Username { get; private set; }
    public string[] Roles { get; private set; }
    public int Expiration { get; private set; } = 1800;

    public JwtTokenModel(string username, params string[] roles)
    {
        Username = username;
        Roles = roles;
    }

    public JwtTokenModel(string username, int expiration, string[] roles)
        : this(username, roles)
    {
        Expiration = expiration;
    }
}
