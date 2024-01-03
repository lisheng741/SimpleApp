using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Simple.Common.EFCore;

public class DefaultEntityDataProvider : IEntityDataProvider
{
    public virtual Guid? UserId => default;

    public virtual Guid? TenantId => default;

    public virtual DateTime? GetCreatedTime()
    {
        return DateTime.Now;
    }

    public virtual Guid? GetCreatedUser()
    {
        return UserId;
    }

    public virtual DateTime? GetUpdatedTime()
    {
        return DateTime.Now;
    }

    public virtual Guid? GetUpdatedUser()
    {
        return UserId;
    }

    public virtual void HandleEntityStateAdded(EntityEntry entry)
    {
    }

    public virtual void HandleEntityStateModified(EntityEntry entry)
    {
    }

    public virtual void HandleEntityStateDeleted(EntityEntry entry)
    {
    }
}
