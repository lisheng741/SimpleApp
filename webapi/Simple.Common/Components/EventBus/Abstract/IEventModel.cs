namespace Simple.Common.EventBus;

/// <summary>
/// 事件模型接口
/// </summary>
public interface IEventModel
{
    Guid Id { get; }

    DateTimeOffset UtcNow { get; }
}
