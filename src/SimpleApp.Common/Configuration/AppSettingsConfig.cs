using Microsoft.Extensions.Configuration;

namespace SimpleApp.Common.Configuration;

/// <summary>
/// 全局静态配置
/// </summary>
public static class AppSettings
{
    private static IConfiguration? _configuration;

    /// <summary>
    /// get or set singleton configuration
    /// </summary>
    public static IConfiguration Configuration
    {
        get
        {
            if (_configuration == null) throw new ArgumentNullException(nameof(Configuration));
            return _configuration;
        }
        set
        {
            _configuration = value;
        }
    }

    #region Jwt 配置
    public static string JwtSecretKey => Configuration["Jwt:SecretKey"];
    public static string JwtIssuer => Configuration["Jwt:Issuer"];
    public static string JwtAudience => Configuration["Jwt:Audience"];
    #endregion

}
