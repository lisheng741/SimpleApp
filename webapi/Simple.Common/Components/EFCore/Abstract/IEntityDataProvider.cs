using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Simple.Common.EFCore;

public interface IEntityDataProvider
{
    Guid? UserId { get; }

    Guid? TenantId { get; }

    DateTime? GetCreatedTime();

    Guid? GetCreatedUser();

    DateTime? GetUpdatedTime();

    Guid? GetUpdatedUser();

    void HandleEntityStateAdded(EntityEntry entry);

    void HandleEntityStateModified(EntityEntry entry);

    void HandleEntityStateDeleted(EntityEntry entry);
}
