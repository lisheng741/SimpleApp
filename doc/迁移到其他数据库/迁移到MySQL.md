# 迁移数据库到MySQL



提供者：[mobei](https://gitee.com/szwscg_admin)



## 操作步骤

### 1 安装 MySQL 数据库提供程序

在项目 `Simple.Repository` 中安装 `MySQL` 数据库提供程序（Provider）的 nuget 包 `Pomelo.EntityFrameworkCore.MySql` 

版本选择6.0.1 版本太高会和EF的版本产生冲突，无法使用

```xml
<ItemGroup>
    <PackageReference Include="Pomelo.EntityFrameworkCore.MySql" Version="6.0.1" />
</ItemGroup>
```

### 2 修改依赖注入

找到项目 `Simple.Repository` 的 Extensions 文件夹下的类：`EfCoreServiceCollectionExtensions` ，修改该类中的 `AddRepository` 方法（**注意**：这里把该方法名改为了 `AddMySqlRepository`），把 `options.UseSqlServer(connectionString); ` 注释掉，换成 `options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));`

关键代码如下：

```csharp
using System.Reflection;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Microsoft.Extensions.DependencyInjection;

public static class EfCoreServiceCollectionExtensions
{
    public static IServiceCollection AddMySqlRepository(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<SimpleDbContext>(options =>
        {
            // 主要是下面这句
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            // 其他代码....
        });

        return services;
    }
}
```

修改项目 `Simple.WebApi` 下的 `Program.cs` 的服务注册：

```csharp
// 仓储层
//builder.Services.AddRepository(configuration["ConnectionStrings:SqlServer"]);
builder.Services.AddMySqlRepository(configuration["ConnectionStrings:MySql"]); 
```

### 3 修改数据库链接字符串

在项目 `Simple.WebApi` 中找到配置文件 `appsettings.json` ，修改 `ConnectionStrings` 节点下的 `MySql` 节点，将其改为自己要连接的数据库的字符串：

```json
"ConnectionStrings": {
    "MySql": "server=localhost;port=3306;database=SimpleApp;user=root;password=123456;charset=utf8mb4;"
}
```

### 4 重新生成项目

重新生成项目，并保证：

- 项目 `Simple.Repository` 的 Migrations 文件夹下面是空的。
- 项目 `Simple.WebApi` 为启动项目

### 5 运行迁移脚本

运行根目录下的 `migrations.sh` 脚本，根据 [使用手册](../使用手册) 进行迁移操作即可。