using NLog;
using NLog.Web;
using Simple.WebApi.CustomApp;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("启动中……");

try
{
    var builder = WebApplication.CreateBuilder(args);

    // --------------------- 配置服务 ---------------------

    // 配置程序所需基本能力
    CustomServiceHelper.AddApplication(builder);

    // 仓储层
    CustomServiceHelper.AddRepository(builder);

    // 服务层
    CustomServiceHelper.AddServices(builder);

    // 表现层
    CustomServiceHelper.AddInterface(builder);

    // 定时任务
    CustomServiceHelper.AddJobScheduling(builder);

    var app = builder.Build();

    // --------------------- 配置 HTTP 请求管道 ---------------------

    // 添加自定义中间件（包含：Body重复读取、异常处理）
    app.UseSimplePipeline();

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
