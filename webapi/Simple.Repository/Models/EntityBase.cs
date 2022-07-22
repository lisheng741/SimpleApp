using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Simple.Repository.Models;

public abstract class EntityBase 
{
    public abstract void ConfigureEntity(EntityTypeBuilder builder);
}

public abstract class EntityBase<TKey> : EntityBase
    where TKey : struct
{
    /// <summary>
    /// 主键
    /// </summary>
    public virtual TKey Id { get; set; }
}

public abstract class BusinessEntityBase<TKey> : EntityBase<TKey>, ISoftDelete, ICreatedInfo, IUpdatedInfo
    where TKey : struct
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

public interface ISoftDelete
{
    public bool IsDeleted { get; set; }
}

public interface ITenant
{
    public Guid TenantId { get; set; }
}

public interface ICreatedTime
{
    public DateTime? CreatedTime { get; set; }
}

public interface ICreatedUser
{
    public Guid? CreatedUserId { get; set; }
}

public interface ICreatedInfo : ICreatedTime, ICreatedUser { }

public interface IUpdatedTime
{
    public DateTime? UpdatedTime { get; set; }
}

public interface IUpdatedUser
{
    public Guid? UpdatedUserId { get; set; }
}

public interface IUpdatedInfo : IUpdatedTime, IUpdatedUser { }
