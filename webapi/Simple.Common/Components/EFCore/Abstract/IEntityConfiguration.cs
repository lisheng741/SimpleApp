using Microsoft.EntityFrameworkCore;

namespace Simple.Common.EFCore;

public interface IEntityConfiguration
{
    void Configure(ModelBuilder builder);
}
