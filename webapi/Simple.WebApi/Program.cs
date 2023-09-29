﻿global using Microsoft.AspNetCore.Http;
global using Microsoft.AspNetCore.Mvc;
global using Simple.Common;
global using Simple.Common.Helpers;
global using Simple.Services;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.OpenApi.Models;
using NLog;
using NLog.Web;
using Simple.Common.Authorization;
using Simple.Common.Models;
using Simple.Services.EventHandlers;
using Simple.Services.Permissions;
using Simple.WebApi;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("启动中……");

try
{
    var builder = WebApplication.CreateBuilder(args);

    // 基本能力配置
    builder.SimpleConfigure();

    var configuration = builder.Configuration;

    // 添加事件总线 (Local)
    builder.Services.AddEventBusLocal().AddSubscriber(subscribers =>
    {
        subscribers.Add<TestEventModel, TestEventHandler>();
        subscribers.Add<ExceptionEvent, ExceptionEventHandler>();
        subscribers.Add<RequestEvent, RequestEventHandler>();
    });

    // API
    builder.Services.AddControllers()
                    .AddDataValidation()
                    .AddApiResult<CustomApiResultProvider>();
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
    // 替换默认 PermissionChecker
    builder.Services.Replace(new ServiceDescriptor(typeof(IPermissionChecker), typeof(PermissionChecker), ServiceLifetime.Transient));

    // 对象映射 AutoMapper
    var profileAssemblies = AssemblyHelper.GetAssemblies(); // 这里读取整个项目程序集，也可以选择只读指定程序集，如: "Simple.Services"
    builder.Services.AddAutoMapper(profileAssemblies, ServiceLifetime.Singleton);

    // 缓存
    builder.Services.AddSimpleCache();

    // JsonOptions
    builder.Services.AddSimpleJsonOptions();

    // 跨域
    builder.Services.AddSimpleCors();

    // 定时任务
    builder.Services.AddJobScheduling(options =>
    {
        options.StartHandle = async sp =>
        {
            var jobService = sp.GetService<IJobService>();
            if (jobService == null) return;
            await jobService.StartAll();
        };
    });

    // 演示环境替换服务 Begin -----------
    // 如果要正常使用系统功能（如：定时任务的功能），请将这部分注释
    builder.Services.Replace(new ServiceDescriptor(typeof(IJobService), typeof(DemoJobService), ServiceLifetime.Transient));
    // 演示环境替换服务 End -----------

    var app = builder.Build();

    // 配置 HTTP 请求管道 --------------------------

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
