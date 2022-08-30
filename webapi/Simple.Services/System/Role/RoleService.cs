namespace Simple.Services;

public class RoleService
{
    private readonly SimpleDbContext _context;
    private readonly CacheService _cacheService;

    public RoleService(SimpleDbContext context, CacheService cacheService)
    {
        _context = context;
        _cacheService = cacheService;
    }

    public async Task<List<RoleModel>> GetAsync()
    {
        var roles = await _context.Set<SysRole>().ToListAsync();
        return MapperHelper.Map<List<RoleModel>>(roles);
    }

    public async Task<PageResultModel<RoleModel>> GetPageAsync(PageInputModel input)
    {
        var result = new PageResultModel<RoleModel>();
        var query = _context.Set<SysRole>().AsQueryable();

        // 根据条件查询
        if (!string.IsNullOrEmpty(input.Code))
        {
            query = query.Where(r => EF.Functions.Like(r.Code, $"%{input.Code}%"));
        }
        if (!string.IsNullOrEmpty(input.Name))
        {
            query = query.Where(r => EF.Functions.Like(r.Name, $"%{input.Name}%"));
        }

        // 获取总数量
        result.TotalRows = await query.CountAsync();

        // 分页查询
        query = query.OrderBy(r => r.Sort).Page(input.PageNo, input.PageSize);
        var roles = await query.ToListAsync();
        result.Rows = MapperHelper.Map<List<RoleModel>>(roles);

        result.SetPage(input);
        result.CountTotalPage();

        return result;
    }

    public async Task<int> AddAsync(RoleModel model)
    {
        if (await _context.Set<SysRole>().AnyAsync(r => r.Id != model.Id && r.Code == model.Code))
        {
            throw AppResultException.Status409Conflict("存在相同编码");
        }

        var role = MapperHelper.Map<SysRole>(model);
        await _context.AddAsync(role);
        return await _context.SaveChangesAsync();
    }

    public async Task<int> UpdateAsync(RoleModel model)
    {
        if (await _context.Set<SysRole>().AnyAsync(r => r.Id != model.Id && r.Code == model.Code))
        {
            throw AppResultException.Status409Conflict("存在相同编码");
        }

        var role = await _context.Set<SysRole>()
            .Where(r => model.Id == r.Id)
            .FirstOrDefaultAsync();

        if (role == null)
        {
            throw AppResultException.Status404NotFound("找不到角色，更新失败");
        }

        MapperHelper.Map<RoleModel, SysRole>(model, role);
        _context.Update(role);
        int ret = await _context.SaveChangesAsync();

        if (ret == 0)
        {
            throw AppResultException.Status200OK("更新记录数为0");
        }

        return ret;
    }

    public async Task<int> DeleteAsync(params Guid[] ids)
    {
        var roles = await _context.Set<SysRole>()
            .Where(r => ids.Contains(r.Id))
            .ToListAsync();

        _context.RemoveRange(roles);
        return await _context.SaveChangesAsync();
    }

    public async Task<List<Guid>> GetMenuIdsAsync(params Guid[] roleIds)
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
        int ret = await _context.SaveChangesAsync();

        // 清缓存
        await _cacheService.ClearRoleCacheAsync(roleId);

        return ret;
    }

    public async Task<List<Guid>> GetDataScopeIdsAsync(params Guid[] roleIds)
    {
        var organizationIds = await _context.Set<SysRole>()
            .Include(r => r.RoleDataScopes)
            .Where(r => roleIds.Contains(r.Id))
            .SelectMany(r => r.RoleDataScopes.Select(rm => rm.OrganizationId))
            .ToListAsync();

        return organizationIds;
    }

    public async Task<int> SetDataScopeAsync(Guid roleId, DataScopeType dataScope, Guid[] organizationIds)
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

        role.DataScope = dataScope;
        role.SetDataScope(organizationIds);

        _context.Update(role);
        return await _context.SaveChangesAsync();
    }
}
