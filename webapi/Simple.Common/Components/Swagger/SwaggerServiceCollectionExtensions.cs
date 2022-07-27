using Microsoft.OpenApi.Models;
using Simple.Common.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Microsoft.Extensions.DependencyInjection;

public static class SwaggerServiceCollectionExtensions
{
    public static IServiceCollection AddSimpleSwagger(this IServiceCollection services, Action<SwaggerGenOptions>? setupAction = null)
    {
        // 默认配置
        Action<SwaggerGenOptions> defaultSetupAction = options =>
        {
            var basePath = AppContext.BaseDirectory;

            //options.SwaggerDoc("v1", new OpenApiInfo { Title = "简单三层接口文档v1", Version = "v1" });

            // 获取根目录下，所有 xml 完整路径（注：并不会获取二级目录下的文件）
            var directoryInfo = new DirectoryInfo(basePath);
            List<string> xmls = directoryInfo
                .GetFiles()
                .Where(f => f.Name.ToLower().EndsWith(".xml"))
                .Select(f => f.FullName)
                .ToList();

            // 添加注释文档
            foreach (var xml in xmls)
            {
                options.IncludeXmlComments(xml);
            }

            // 开启加权小锁
            options.OperationFilter<AuthenticationOperationFilter>();

            // 接入 Jwt 认证
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Scheme = "Bearer",
                BearerFormat = "JWT",
                Description = "在下面输入框输入Token",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http
            });
        };

        // 注册 Swagger 并添加默认配置
        services.AddSwaggerGen(defaultSetupAction);

        // 如果有自定义配置
        if (setupAction != null) services.Configure(setupAction);

        return services;
    }
}
