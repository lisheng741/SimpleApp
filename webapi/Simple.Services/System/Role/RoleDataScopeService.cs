namespace Simple.Services;

public class RoleDataScopeService
{
    private readonly SimpleDbContext _context;

    public RoleDataScopeService(SimpleDbContext context)
    {
        _context = context;
    }

    public async Task<List<Guid>> GetDataScopeAsync(params Guid[] roleIds)
    {
        var organizationIds = await _context.Set<SysRole>()
            .Include(r => r.RoleDataScopes)
            .Where(r => roleIds.Contains(r.Id))
            .SelectMany(r => r.RoleDataScopes.Select(rm => rm.OrganizationId))
            .ToListAsync();

        return organizationIds;
    }


    public async Task<int> SetDataScopeAsync(Guid roleId, Guid[] organizationIds)
    {
        // 查找
        var role = await _context.Set<SysRole>()
            .Include(r => r.RoleDataScopes)
            .Where(r => r.Id == roleId)
            .FirstOrDefaultAsync();

        if (role == null)
        {
            throw AppResultException.Status404NotFound("找不到角色，设置失败");
        }

        role.SetDataScope(organizationIds);

        _context.Update(role);
        return await _context.SaveChangesAsync();
    }
}
