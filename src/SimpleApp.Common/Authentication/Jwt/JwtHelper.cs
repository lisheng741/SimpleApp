using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace SimpleApp.Common.Authentication.Jwt;

public class JwtHelper
{
    public static string IssueJwt(JwtTokenModel tokenModel)
    {
        // 获取配置
        string iss = AppSettingsConfig.JwtIssuer;
        string aud = AppSettingsConfig.JwtAudience;
        string secret = AppSettingsConfig.JwtSecretKey;

        var claims = new List<Claim>()
        {
            new Claim(JwtClaimTypes.Username, tokenModel.Username),
        };

        // 密钥
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var jwt = new JwtSecurityToken(claims: claims);

        var jwtHandler = new JwtSecurityTokenHandler();
        var encodedJwt = jwtHandler.WriteToken(jwt);

        return encodedJwt;
    }
}
