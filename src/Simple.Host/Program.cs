global using Simple.Common.Configuration;
global using Simple.Common.Helpers;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
AppSettings.Configuration = configuration;

// Add services to the container.
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// JsonOptions
builder.Services.AddJsonOptions();

// 对象映射 AutoMapper
var profileAssemblies = AssemblyHelper.GetAssemblies("Simple.Services");
builder.Services.AddAutoMapper(profileAssemblies);

// 仓储
builder.Services.AddRepository(configuration["ConnectionStrings:SqlServer"]);

// 服务：自动添加 Service 层以 Service 结尾的服务
builder.Services.AddServices("Simple.Services");

// Cookie 认证
builder.Services.AddCookieAuthentication();
// 授权
builder.Services.AddSimpleAuthorization();

// 缓存
builder.Services.AddCache();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
