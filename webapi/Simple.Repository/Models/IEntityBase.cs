namespace Simple.Repository.Models;

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
