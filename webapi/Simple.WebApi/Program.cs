global using Microsoft.AspNetCore.Http;
global using Microsoft.AspNetCore.Mvc;
global using Simple.Common;
global using Simple.Common.Configuration;
global using Simple.Common.Helpers;
global using Simple.Services;
using Microsoft.OpenApi.Models;
using NLog;
using NLog.Web;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("启动中……");

try
{
    var builder = WebApplication.CreateBuilder(args);

    var configuration = builder.Configuration;
    AppSettings.Configure(configuration);

    // 添加 HostedService
    builder.Services.AddHostedService<SimpleHostedService>();

    // 日志
    //builder.Logging.ClearProviders(); // .AddConsole()
    builder.Host.UseNLog();

    // API
    builder.Services.AddControllers()
        .AddDataValidation()
        .AddAppResult(options =>
        {
            options.ResultFactory = resultException =>
            {
                // Api 结果
                IActionResult result = new ContentResult()
                {
                    // AppResultException 都返回 200 状态码
                    StatusCode = StatusCodes.Status200OK,
                    ContentType = "application/json; charset=utf-8",
                    Content = JsonHelper.Serialize(resultException.AppResult)
                };

                return result;
            };
        });
    builder.Services.AddEndpointsApiExplorer();

    // Swagger
    builder.Services.AddSimpleSwagger(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo { Title = "简单三层接口文档v1", Version = "v1" });
    });

    // 仓储层
    builder.Services.AddRepository(configuration["ConnectionStrings:SqlServer"]);

    // 服务层：添加基础服务
    builder.Services.AddSimpleBaseServices();
    // 服务层：自动添加 Service 层以 Service 结尾的服务
    builder.Services.AddAutoServices("Simple.Services");

    //// Cookie 认证
    //builder.Services.AddCookieAuthentication();
    // JWT 认证
    builder.Services.AddJwtAuthentication();
    // 授权
    builder.Services.AddSimpleAuthorization();

    // 对象映射 AutoMapper
    var profileAssemblies = AssemblyHelper.GetAssemblies("Simple.Services");
    builder.Services.AddAutoMapper(profileAssemblies);

    // 缓存
    builder.Services.AddSimpleCache();

    // JsonOptions
    builder.Services.AddSimpleJsonOptions();

    // 跨域
    builder.Services.AddSimpleCors();

    var app = builder.Build();

    // 配置 HTTP 请求管道
    // 全局异常处理
    app.UseApiException();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Simple API v1");
        });
    }

    app.UseHttpsRedirection();

    // UseCors 必须在 UseRouting 之后，UseResponseCaching、UseAuthorization 之前
    app.UseCors();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    logger.Error(ex, "由于发生异常，导致程序中止！");
    throw;
}
finally
{
    LogManager.Shutdown();
}
