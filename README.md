# 简单三层应用 SimpleApp

简单三层，力求精简。

## 前言

个人2022年7月15日从公司离职以后，并没有急着找下一份工作，而是希望把这两年的工作经历，以及想法做一个沉淀，故而有了这个项目。

实际上，个人2021年年初曾参考一个模块化的项目（[SimplCommerce](https://github.com/simplcommerce/SimplCommerce)），试着从0到1搭建一个完整的项目，并应用在工作的项目里（当时就我一个开发，想搞啥搞啥，可太爽了）。今年再看那个项目，虽说结构、代码都很干净，但很多想法及实现还是太稚嫩了。而且模块化的架构，实际上只是更贴近我当时（上上家）的业务场景，不太适应通用的情况。

新开的这个项目，期望实现这样的能力：业务人员只需关注实体的构建，业务服务的编写，以及路由的配置。

让业务的开发，变成简单的三步走：创建实体 >> 业务开发 >> 路由配置。

## 项目概述

前后端分离，使用 JWT 认证。

后端：基于 .NET6 和 EF Core，集成常用组件。

前端：基于 [小诺1.8](https://gitee.com/xiaonuobase/snowy) 做适配，主技术栈：Vue2.6.x、Ant-Design-Vue

## 体验地址



## 参考项目

[小诺 snowy](https://gitee.com/xiaonuobase/snowy)

[Admin.NET](https://gitee.com/zuohuaijun/Admin.NET)

[Blog.Core](https://gitee.com/laozhangIsPhi/Blog.Core)

[Adnc](https://github.com/AlphaYu/Adnc)

感谢这些优秀的开源项目！

## 项目结构

基本结构如下，详细的结构，请查看文档。

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

笔者认为，对于单体项目来说，做到高内聚即可，再追求完全的低耦合，会增加成本和困扰（举个简单的栗子：项目初期，业务大改是常有的事，改服务类的接口的事并不少见。除非说业务主体明确，需要修改的，并不是业务的接口，也是业务的具体实现）。

最后是这个项目，本就是为了追求最简三层单体。

#### 为什么不对仓储额外封装一层？

答：简单的项目基本上是单数据库的，且 EF Core 已经实现了工作单元和仓储模式，可以不用再封装一层。

当然，笔者还是建议跟ABP框架那样再封装一层仓储，可以避免一些后续的开发运维问题（比如：系统迁移、重构等）。

