using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

namespace SimpleApp.Common.Authentication.Jwt;

public static class JwtServiceCollectionExtensions
{
    public static IServiceCollection AddJwt(this IServiceCollection services)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));

        //配置
        var symmetricKeyAsBase64 = AppSettingsConfig.JwtSecretKey;
        var issuer = AppSettingsConfig.JwtIssuer;
        var audience = AppSettingsConfig.JwtAudience;

        var keyByteArray = Encoding.UTF8.GetBytes(symmetricKeyAsBase64);
        var signingKey = new SymmetricSecurityKey(keyByteArray);
        var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

        // 令牌验证参数
        var tokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuerSigningKey = true, // 是否验证SecurityKey
            IssuerSigningKey = signingKey, // 拿到SecurityKey
            ValidateIssuer = true, // 是否验证Issuer
            ValidIssuer = issuer, // 发行人Issuer
            ValidateAudience = true, // 是否验证Audience
            ValidAudience = audience, // 订阅人Audience
            ValidateLifetime = true, // 是否验证失效时间
            ClockSkew = TimeSpan.FromSeconds(30), // 过期时间容错值，解决服务器端时间不同步问题（秒）
            RequireExpirationTime = true,
            NameClaimType = JwtClaimTypes.Username,
            RoleClaimType = JwtClaimTypes.Role,
        };

        // 开启Bearer认证
        services.AddAuthentication(options =>
        {
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            //options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //options.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = tokenValidationParameters;
            options.Events = new JwtBearerEvents()
            {
                OnChallenge = async context =>
                {
                    await context.Response.WriteAsync("401");
                },
                OnForbidden = async context =>
                {
                    await context.Response.WriteAsync("403");
                }
            };
        });

        return services;
    }
}
