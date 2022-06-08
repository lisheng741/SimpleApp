using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace SimpleApp.Common.Configuration;

public class AppSettingsConfig
{
    public static IConfiguration Configuration { get; private set; }

    public AppSettingsConfig(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    #region Jwt 配置
    public static readonly string JwtSecretKey = Configuration!["Jwt:SecretKey"];
    public static readonly string JwtIssuer = Configuration!["Jwt:Issuer"];
    public static readonly string JwtAudience = Configuration!["Jwt:Audience"];
    #endregion

}
