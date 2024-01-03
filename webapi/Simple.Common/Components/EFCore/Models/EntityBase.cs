namespace Simple.Common.EFCore.Models;

/// <summary>
/// 基础实体
/// </summary>
public abstract class EntityBase : IEntity
{
}

/// <summary>
/// 带主键的基础实体
/// </summary>
/// <typeparam name="TKey"></typeparam>
public abstract class EntityBase<TKey> : EntityBase
    where TKey : struct, IEquatable<TKey>
{
    /// <summary>
    /// 主键
    /// </summary>
    public virtual TKey Id { get; set; }
}
