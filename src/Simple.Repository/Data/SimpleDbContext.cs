namespace Simple.Repository.Data;

public class SimpleDbContext : DbContext, ISimpleDbContext
{
    public SimpleDbContext(DbContextOptions<SimpleDbContext> options)
        : base(options)
    { }

    public virtual DbSet<SysRole> SysRole { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<SysRole>();
    }
}
