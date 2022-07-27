global using Simple.Common.Configuration;
global using Simple.Common.Helpers;
using Microsoft.OpenApi.Models;
using Simple.Common;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
AppSettings.Configure(configuration);

// API
builder.Services.AddControllers();
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

// 模型验证
builder.Services.AddDataValidation();

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

// 初始化配置，主要是配置静态对象
SimpleStartup.Configure(app);

// 配置 HTTP 请求管道
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
