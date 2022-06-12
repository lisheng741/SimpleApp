using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace SimpleApp.Common.Components.Authentication.Jwt;

public class JwtHelper
{
    public static string IssueJwt(JwtTokenModel tokenModel)
    {
        // 获取配置
        string issuer = AppSettings.JwtIssuer;
        string audience = AppSettings.JwtAudience;
        string secret = AppSettings.JwtSecretKey;

        var claims = new List<Claim>()
        {
            new Claim(JwtClaimTypes.Username, tokenModel.Username),
            new Claim(JwtClaimTypes.JwtId, tokenModel.Username),
            new Claim(JwtClaimTypes.IssuedAt, DateHelper.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
            new Claim(JwtClaimTypes.NotBefore, DateHelper.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
            new Claim(JwtClaimTypes.Expiration, DateHelper.ToUnixTimeSeconds(DateTime.Now.AddSeconds(tokenModel.Expiration)).ToString(), ClaimValueTypes.Integer64),
            new Claim(JwtClaimTypes.Issuer, issuer),
            new Claim(JwtClaimTypes.Audience, audience),
        };

        //var claims = new List<Claim>()
        //{
        //    // 这两种方式，都不行
        //    new Claim(JwtClaimTypes.IssuedAt, DateTime.Now.ToString()),
        //    new Claim(JwtClaimTypes.NotBefore, $"{DateHelper.ToUnixTimeSeconds()}"),
        //};

        foreach (var role in tokenModel.Roles)
        {
            if (string.IsNullOrEmpty(role)) continue;
            claims.Add(new Claim(JwtClaimTypes.Role, role));
        }

        // 密钥
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        // 生成 token
        var jwt = new JwtSecurityToken(claims: claims,signingCredentials: creds);
        var jwtHandler = new JwtSecurityTokenHandler();
        var encodedJwt = jwtHandler.WriteToken(jwt);

        return encodedJwt;
    }
}
