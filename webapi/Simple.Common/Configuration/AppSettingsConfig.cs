using Microsoft.Extensions.Configuration;

namespace Simple.Common.Configuration;

/// <summary>
/// 全局静态配置
/// </summary>
public static class AppSettings
{
    private static IConfiguration? _configuration;

    /// <summary>
    /// 获取 Configuration 的单例
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
            if (_configuration != null) throw new Exception($"{nameof(Configuration)}只能设置一次");
            _configuration = value ?? throw new ArgumentNullException(nameof(value));
        }
    }

    public static string[] AllowCors => Configuration.GetSection("AllowCors").Get<string[]>();

    #region Jwt 配置
    public static string JwtSecretKey => Configuration["Jwt:SecretKey"];
    public static string JwtIssuer => Configuration["Jwt:Issuer"];
    public static string JwtAudience => Configuration["Jwt:Audience"];
    #endregion


    #region Redis
    public static bool RedisEnabled => Configuration.GetValue<bool>("Redis:Enabled");
    public static string RedisConnectionString => Configuration["Redis:ConnectionString"];
    public static string RedisInstance => Configuration["Redis:Instance"] ?? "Default";
    #endregion
}
