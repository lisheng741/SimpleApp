namespace Simple.Common.EFCore.Models;

/// <summary>
/// 业务基础实体
/// </summary>
/// <typeparam name="TKey"></typeparam>
public abstract class BusinessEntityBase<TKey> : EntityBase<TKey>, ISoftDelete, ICreatedInfo, IUpdatedInfo
    where TKey : struct, IEquatable<TKey>
{
    /// <summary>
    /// 软删标记
    /// </summary>
    public virtual bool IsDeleted { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public virtual DateTime? CreatedTime { get; set; }

    /// <summary>
    /// 创建用户Id
    /// </summary>
    public virtual Guid? CreatedUserId { get; set; }

    /// <summary>
    /// 更新时间
    /// </summary>
    public virtual DateTime? UpdatedTime { get; set; }

    /// <summary>
    /// 更新用户Id
    /// </summary>
    public virtual Guid? UpdatedUserId { get; set; }
}
