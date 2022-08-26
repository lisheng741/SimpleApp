namespace Simple.Services;

public class RoleMenuService
{
    private readonly SimpleDbContext _context;

    public RoleMenuService(SimpleDbContext context)
    {
        _context = context;
    }

    public async Task<List<Guid>> GetMenuAsync(params Guid[] roleIds)
    {
        var menuIds = await _context.Set<SysRole>()
            .Include(r => r.RoleMenus)
            .Where(r => roleIds.Contains(r.Id))
            .SelectMany(r => r.RoleMenus.Select(rm => rm.MenuId))
            .ToListAsync();

        return menuIds;
    }


    public async Task<int> SetMenuAsync(Guid roleId, Guid[] menuIds)
    {
        // 查找
        var role = await _context.Set<SysRole>()
            .Include(r => r.RoleMenus)
            .Where(r => r.Id == roleId)
            .FirstOrDefaultAsync();

        if (role == null)
        {
            throw AppResultException.Status404NotFound("找不到角色，设置失败");
        }

        role.SetMenu(menuIds);

        _context.Update(role);
        return await _context.SaveChangesAsync();
    }
}
