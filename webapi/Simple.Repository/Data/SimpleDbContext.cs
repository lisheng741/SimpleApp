using Simple.Common.EFCore;

namespace Simple.Repository;

public class SimpleDbContext : SimpleBaseDbContext
{
    public SimpleDbContext(DbContextOptions<SimpleDbContext> options,
                           IEntityDataProvider dataProvider)
        : base(options, dataProvider)
    {
    }

    public DbSet<SysApplication> SysApplication { get; set; } = null!;

    public DbSet<SysDictionary> SysDictionary { get; set; } = null!;

    public DbSet<SysDictionaryItem> SysDictionaryItem { get; set; } = null!;

    public DbSet<SysJob> SysJob { get; set; } = null!;

    public DbSet<SysLogException> SysLogException { get; set; } = null!;

    public DbSet<SysLogOperating> SysLogOperating { get; set; } = null!;

    public DbSet<SysMenu> SysMenu { get; set; } = null!;

    public DbSet<SysOrganization> SysOrganization { get; set; } = null!;

    public DbSet<SysPosition> SysPosition { get; set; } = null!;

    public DbSet<SysRole> SysRole { get; set; } = null!;

    public DbSet<SysRoleDataScope> SysRoleDataScope { get; set; } = null!;

    public DbSet<SysRoleMenu> SysRoleMenu { get; set; } = null!;

    public DbSet<SysUser> SysUser { get; set; } = null!;

    public DbSet<SysUserDataScope> SysUserDataScope { get; set; } = null!;

    public DbSet<SysUserRole> SysUserRole { get; set; } = null!;
}
