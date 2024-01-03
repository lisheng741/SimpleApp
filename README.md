# 简单三层应用 SimpleApp

简单三层，力求精简。

.NET Core / .NET8 入门级项目。

关键词：入门级、通用后台管理系统、前后端分离、用户权限管理系统。

QQ交流群：694414280（欢迎广大 .NET 开发者加入）

## 前言

本项目定位：开箱即用的中小型管理系统，前后端分离（不再包含MVC相关内容），清晰的项目结构，入门级但漂亮的代码。

这个项目，期望实现让业务的开发，变成简单的三步走：创建实体 >> 业务开发 >> 路由配置。

希望有兴趣的小伙伴加入这个项目的完善。

## 项目概述

前后端分离，使用 JWT 认证。

后端：基于 .NET8 和 EF Core，集成常用组件，从0到1搭建。

前端：基于 [小诺1.8](https://gitee.com/xiaonuobase/snowy) 做适配，主技术栈：Vue2.6.x、Ant-Design-Vue

## 体验地址

http://47.101.134.134:12060/

管理员：superAdmin  密码：123456

普通用户：user  密码：123456

目前使用部署方式：Nginx 运行前端、dotnet 命令运行后端。

## 快速开始

请参考[使用手册](./doc/使用手册.md)

本人本地开发环境：

VS2022

VS Code

SQL Server2019 Express

.NET8

node 16.13.2

## 参考项目

注：排名不分先后。

[小诺 snowy](https://gitee.com/xiaonuobase/snowy)

[Admin.NET](https://gitee.com/zuohuaijun/Admin.NET)

[Blog.Core](https://gitee.com/laozhangIsPhi/Blog.Core)

[Adnc](https://github.com/AlphaYu/Adnc)

[Furion](https://gitee.com/dotnetchina/Furion)

[ABP](https://github.com/abpframework/abp)

感谢这些优秀的开源项目！

## 基本设计思路

- 依赖于抽象

  依赖倒置原则，控制反转（IoC）

- 切面编程（AOP）

  权限、日志、异常等通过过滤器（Filter）或中间件（Middleware）等实现，集中编程

- 可配置

- 自动注册

  自动注册实体（Entity）、自动注册服务类（Service）等

## 项目结构

### 项目结构构思

![image-20220916003303757](./doc/images/image-20220916003303757.png)

**主要分为三层：Interface表现层、Services服务层、Repository仓储层**

**Interface**：Host依赖所有层，完成程序配置（如：Program.cs 中DI容器注入服务，中间件管道配置等）；Web API 配置路由，提供 API 接口，如果程序以后有迁移、或替换前端的情况，也可以在这里做一层适配器（注：API只是一种表现形式，也可以为MVC）

**Services**：所有的业务都在这一层。从仓储中读取数据模型（Models），进行业务操作，返回DTO（Data transfer objects）给表现层。

**Repository**：数据库访问。

**通用的模块：Model、Common、Framework**

**Models**：包含所有数据模型，如 Entity（对象数据库的数据表）、CacheItem缓存对象、EventModel事件模型等。

**Common**：集成常用组件，根据项目需要做相应配置；提供基础服务，如CurrentUser访问当前用户信息；提供静态帮助类，所有无状态的函数都归入此类，如`GuidHelper.Next()` 产生连续 Guid。

**Framework**：框架，比如引用ABP或Furion等框架，甚至是自己项目一些通用的能力，可以到处用的。

### 实际项目结构

实际上，把 IServices 和 IRepository 此类接口层干掉了。

Models 则归入了对应的使用者里面，Framework 也没有。

```bash
Common        # 基础设施：集成常用组件；提供基础服务；提供静态帮助类
Repository    # 仓储层：数据库访问，数据库迁移
Services      # 服务层：业务实现
WebApi        # 表现层：完成程序配置；配置路由，提供API接口
```

目录结构如下，更详细的结构，请查看文档。

```bash
├─config                  # 一些配置文件，如：redis 的配置文件
├─doc                     # 项目文档
├─web                     # 前端
├─webapi                  # 后端
   ├─Simple.Common        # 基础设施
   ├─Simple.Repository    # 仓储层
   ├─Simple.Services      # 服务层
   └─Simple.WebApi        # 表现层
```

## 业务能力

- 组织架构
  - 组织机构（organization）
  - 岗位（position）
  - 用户（user）
- 权限管理
  - 应用（application）
  - 菜单（menu）
  - 角色（role）
- 开发管理
  - 数据字典（dictionary、dictionaryItem）
- 日志管理
  - 操作日志（log operating）
  - 异常日志（log exception）

## 系统能力

- 认证：集成Cookies、JWT；默认启用 JWT
- 授权：[基于策略（Policy）的授权](https://docs.microsoft.com/zh-cn/aspnet/core/security/authorization/policies?view=aspnetcore-6.0)
- ORM：[EF Core](https://docs.microsoft.com/zh-cn/ef/core/) 的 [Code First 模式](https://docs.microsoft.com/zh-cn/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli)
- 依赖注入：默认 DI 容器，实现自动注入
- 缓存：[IDistributedCache](https://docs.microsoft.com/zh-cn/dotnet/api/microsoft.extensions.caching.distributed.idistributedcache)，默认注入 Memory Cache，可替换 Redis
- 日志：[NLog](https://nlog-project.org/)
- 事件总线：[默认启用 BackgroupService](https://docs.microsoft.com/zh-cn/dotnet/core/extensions/queue-service?source=recommendations)，基于[Channel](https://docs.microsoft.com/zh-cn/dotnet/api/system.threading.channels.channel-1) 实现的单机版发布订阅；可替换为 Redis 的发布订阅（可用于分布式）；也可替换为 RabbitMQ 的发布订阅（可用于分布式）
- 定时任务：Quartz
- 数据验证：[模型验证（Model validation）](https://docs.microsoft.com/zh-cn/aspnet/core/mvc/models/validation)
- 对象映射：AutoMapper

## 项目亮点

- [EF Core 自动迁移脚本](./webapi/migrations.sh)

## 一些Q&A

#### 为什么把 IServices 这些接口层给干掉了，仅留下实现层？

答：一般项目中会如有 IRepository 和 IServices 这些个抽象层，主要是为了控制反转（IoC），实现项目各层之间解耦，最终目的就是为了“高内聚，低耦合”。

笔者认为，对于单体项目来说，做到高内聚即可，再追求完全的低耦合，会增加成本和困扰（举个简单的栗子：项目初期，业务大改是常有的事，改服务类的接口的事并不少见。除非说业务主体明确，需要修改的，并不是业务的接口，而是业务的具体实现）。

最后是这个项目，本就是为了追求最简三层单体。

#### 为什么不对仓储额外封装一层？

答：简单的项目基本上是单数据库的，且 EF Core 已经实现了工作单元和仓储模式，可以不用再封装一层。

当然，笔者还是建议跟ABP框架那样再封装一层仓储，可以避免一些后续的开发运维问题（比如：系统迁移、重构等）。

## 贡献

- 提 Issue 请到 Gitee

## 捐助

如果这个项目对您有所帮助，可以扫下方二维码打赏一杯咖啡。

啊呸，我不喝咖啡，来杯可乐吧，啊哈哈哈。

[赞助列表](https://lisheng741.gitee.io/blog/thanks/)

![image-20220916003303757](./doc/images/payforme.png)

