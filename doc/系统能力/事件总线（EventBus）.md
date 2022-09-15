# 事件总线（EventBus）

集成了本地（Local）、Redis、RabbitMQ三种发布/订阅方式



## 1 启用事件总线

在 Program.cs 中，添加事件总线服务：

```csharp
// 启动本地事件总线
builder.Services.AddEventBusLocal();
```

注：总线集成了3种实现，只需启用其中一种即可。

如果添加的是 Redis 或 RabbitMQ 实现的事件总线，需要输入相应的配置，如：

```csharp
builder.Services.AddEventBusRedis(options =>
{
    options.Configuration = "127.0.0.1:6379,password=123456";
});
```



## 2 添加消费者

添加消费者需先准备事件模型（EventModel）和处理程序（EventHandler）

### （1）事件模型（EventModel）

事件模型必须实现 IEventModel 接口且为 class 类型，或继承抽象类 EventModel.

```csharp
public class TestEventModel : EventModel
{
    public string Message { get; set; } = "测试消息";
}
```

### （2）处理程序（EventHandler）

处理程序必须实现泛型接口 IEventHandler<> ，其中泛型为（1）中准备的事件模型（EventModel）.

```csharp
public class TestEventHandler : IEventHandler<TestEventModel>
{
    public Task Handle(TestEventModel @event)
    {
        Console.WriteLine($"收到信息：{@event.Message}");
        return Task.CompletedTask;
    }
}
```

### （3）添加消费者

在 Program.cs 中，在添加事件总线服务后，会获得一个 EventBusBuilder 类型的实例，使用该实例在 WebApplicationBuilder.Build() 方法执行前添加消费者，如：

```csharp
builder.Services.AddEventBusLocal().AddSubscriber(subscribers =>
{
    subscribers.Add<TestEventModel, TestEventHandler>();
});
```

或：

```csharp
var eventBusBuilder = builder.Services.AddEventBusLocal();

eventBusBuilder.AddSubscriber(subscribers =>
{
    subscribers.Add<TestEventModel, TestEventHandler>();
});
```



## 3 发布事件

使用 IEventPublisher.PublishAsync() 发布事件：

```csharp
var @event = new TestEventModel();
await _eventPublisher.PublishAsync(@event);
```

发布以后，对应的消费者处理程序，将会处理
