namespace Simple.Repository.Models;

public abstract class EntityBase
{
}

public abstract class EntityBase<TKey> : EntityBase
    where TKey: struct
{
    public virtual TKey Id { get; set; }
}

public interface ISoftDelete
{
    public bool IsDeleted { get; set; }
}
