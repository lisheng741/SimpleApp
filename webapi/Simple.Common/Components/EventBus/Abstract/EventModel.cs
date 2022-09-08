namespace Simple.Common.EventBus;

/// <summary>
/// 基础事件模型
/// </summary>
public abstract class EventModel : IEventModel
{
    /// <summary>
    /// 事件模型唯一Id。
    /// </summary>
    public virtual Guid Id { get; set; } = GuidHelper.Next(); // Guid.NewGuid();

    /// <summary>
    /// 创建事件对象时的 Utc 时间。
    /// </summary>
    public virtual DateTimeOffset UtcNow { get; set; } = DateTimeOffset.UtcNow;

    public EventModel()
    {
        
    }

    public EventModel(Guid id, DateTimeOffset utcNow)
    {
        Id = id;
        UtcNow = utcNow;
    }
}
