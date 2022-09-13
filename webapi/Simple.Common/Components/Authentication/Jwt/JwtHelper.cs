using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace Simple.Common.Authentication.Jwt;

public static class JwtHelper
{
    /// <summary>
    /// 生成 JWT Token
    /// </summary>
    /// <param name="tokenModel"></param>
    /// <returns></returns>
    public static string Create(JwtTokenModel tokenModel)
    {
        // 获取配置
        string issuer = AppSettings.Jwt.Issuer;
        string audience = AppSettings.Jwt.Audience;
        string secret = AppSettings.Jwt.SecretKey;

        var claims = new List<Claim>()
        {
            new Claim(SimpleClaimTypes.UserName, tokenModel.UserName),
            new Claim(SimpleClaimTypes.JwtId, tokenModel.UserName),
            new Claim(SimpleClaimTypes.IssuedAt, DateTime.Now.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
            new Claim(SimpleClaimTypes.NotBefore, DateTime.Now.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
            new Claim(SimpleClaimTypes.Expiration, DateTime.Now.AddSeconds(tokenModel.Expiration).ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
            new Claim(SimpleClaimTypes.Issuer, issuer),
            new Claim(SimpleClaimTypes.Audience, audience),
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
            claims.Add(new Claim(SimpleClaimTypes.Role, role));
        }

        if(tokenModel.Claims.Count > 0)
        {
            claims.AddRange(tokenModel.Claims);
        }

        // 密钥
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        // 生成 token
        var jwt = new JwtSecurityToken(claims: claims, signingCredentials: creds);
        var jwtHandler = new JwtSecurityTokenHandler();
        var encodedJwt = jwtHandler.WriteToken(jwt);

        return encodedJwt;
    }
}
