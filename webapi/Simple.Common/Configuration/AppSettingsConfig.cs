using Microsoft.Extensions.Caching.Distributed;
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
            if (_configuration == null) throw new NullReferenceException(nameof(Configuration));
            return _configuration;
        }
    }

    [Obsolete("这只是一个示例，请使用 Configure(IConfiguration) 方法")]
    public static void Configure(IServiceProvider serviceProvider)
    {
        if (serviceProvider == null) throw new ArgumentNullException(nameof(serviceProvider));

        var configuration = serviceProvider.GetService<IConfiguration>();

        Configure(configuration);
    }

    /// <summary>
    /// 设置 Configuration 的实例
    /// </summary>
    /// <param name="configuration"></param>
    /// <exception cref="Exception"></exception>
    /// <exception cref="ArgumentNullException"></exception>
    public static void Configure(IConfiguration? configuration)
    {
        if (_configuration != null)
        {
            throw new Exception($"{nameof(Configuration)}不可修改！");
        }
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    /// <summary>
    /// 是否记录操作日志
    /// </summary>
    public static bool IsRecordRequestLog => Configuration.GetValue<bool>("IsRecordRequestLog");

    /// <summary>
    /// 允许跨域请求列表
    /// </summary>
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
