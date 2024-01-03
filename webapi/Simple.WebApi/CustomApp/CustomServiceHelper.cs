using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Simple.Common.Authorization;
using Simple.Common.EFCore;
using Simple.Common.Models;
using Simple.Repository;
using Simple.Services.EventHandlers;
using Simple.Services.Permissions;
using Simple.Services.Providers;

namespace Simple.WebApi.CustomApp
{
    public static class CustomServiceHelper
    {
        public static void AddApplication(WebApplicationBuilder builder)
        {
            // 基本能力配置
            builder.SimpleConfigure();

            // 添加事件总线 (Local)
            builder.Services.AddEventBusLocal().AddSubscriber(subscribers =>
            {
                subscribers.Add<TestEventModel, TestEventHandler>();
                subscribers.Add<ExceptionEvent, ExceptionEventHandler>();
                subscribers.Add<RequestEvent, RequestEventHandler>();
            });

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
        }

        public static void AddRepository(WebApplicationBuilder builder)
        {
            // 添加 EF Core 相关能力
            builder.Services.AddSimpleEFCore();

            var connectionString = builder.Configuration["ConnectionStrings:SqlServer"];
            builder.Services.AddDbContext<SimpleDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            // 替换默认 EntityDataProvider
            builder.Services.Replace(new ServiceDescriptor(typeof(IEntityDataProvider), typeof(EntityDataProvider), ServiceLifetime.Scoped));
        }

        public static void AddServices(WebApplicationBuilder builder)
        {
            // 服务层：添加基础服务
            builder.Services.AddSimpleBaseServices();

            // 服务层：自动添加 Service 层以 Service 结尾的服务
            builder.Services.AddAutoServices("Simple.Services");
        }

        public static void AddInterface(WebApplicationBuilder builder)
        {
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

            // 跨域
            builder.Services.AddSimpleCors();
        }

        public static void AddJobScheduling(WebApplicationBuilder builder)
        {
            // Quartz 定时任务
            builder.Services.AddQuartzJobScheduling(options =>
            {
                options.StartHandle = async sp =>
                {
                    var jobService = sp.GetService<IJobService>();
                    if (jobService == null) return;
                    await jobService.StartAll();
                };
            });

            // 演示环境替换服务
            // 如果要正常使用系统功能，请将这部分注释
            builder.Services.Replace(new ServiceDescriptor(typeof(IJobService), typeof(DemoJobService), ServiceLifetime.Transient));
        }
    }
}
