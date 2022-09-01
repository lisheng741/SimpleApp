namespace Simple.Services;

public class MenuService
{
    private readonly ISimpleService _services;
    private readonly SimpleDbContext _context;
    private readonly CacheService _cacheService;

    public MenuService(SimpleDbContext context, 
                       ISimpleService services,
                       CacheService cacheService)
    {
        _context = context;
        _services = services;
        _cacheService = cacheService;
    }

    public async Task<List<MenuTreeNodeModel>> GetAsync(MenuInputModel input)
    {
        var query = _context.Set<SysMenu>().AsQueryable();

        // 根据条件查询
        if (!string.IsNullOrEmpty(input.Name))
        {
            query = query.Where(m => EF.Functions.Like(m.Name, $"%{input.Name}%"));
        }
        if (!string.IsNullOrEmpty(input.Application))
        {
            query = query.Where(m => m.Application == input.Application);
        }

        // 排序
        query = query.OrderBy(m => m.Sort);

        var menus = await query.ToListAsync();
        var nodes = MapperHelper.Map<List<MenuTreeNodeModel>>(menus);

        var builder = MenuTreeNodeModel.CreateBuilder(nodes);
        return builder.Build();
    }

    public async Task<PageResultModel<MenuModel>> GetPageAsync(PageInputModel input)
    {
        var result = new PageResultModel<MenuModel>();
        var query = _context.Set<SysMenu>().AsQueryable();

        // 根据条件查询
        if (!string.IsNullOrEmpty(input.Name))
        {
            query = query.Where(m => EF.Functions.Like(m.Name, $"%{input.Name}%"));
        }

        // 排序
        query = query.OrderBy(m => m.Sort);

        // 获取总数量
        result.TotalRows = await query.CountAsync();

        // 分页查询
        query = query.Page(input.PageNo, input.PageSize);
        var menus = await query.ToListAsync();
        result.Rows = MapperHelper.Map<List<MenuModel>>(menus);

        result.SetPage(input);
        result.CountTotalPage();

        return result;
    }

    public async Task<List<AntTreeNode>> GetTreeAsync(bool isContainsButton = false)
    {
        var query = _context.Set<SysMenu>().AsQueryable();

        if (!isContainsButton)
        {
            query = query.Where(m => m.Type != MenuType.Button);
        }

        var menus = await query
            .OrderBy(m => m.Sort)
            .ToListAsync();
        List<TreeNode> nodes = MapperHelper.Map<List<TreeNode>>(menus);

        var builder = AntTreeNode.CreateBuilder(nodes);
        return builder.Build();
    }

    public async Task<int> AddAsync(MenuModel model)
    {
        if (await _context.Set<SysMenu>().AnyAsync(m => m.Code == model.Code))
        {
            throw AppResultException.Status409Conflict("存在相同编码的组织");
        }

        var menu = MapperHelper.Map<SysMenu>(model);
        await _context.AddAsync(menu);
        return await _context.SaveChangesAsync();
    }

    public async Task<int> UpdateAsync(MenuModel model)
    {
        if (await _context.Set<SysMenu>().AnyAsync(m => m.Id != model.Id && m.Code == model.Code))
        {
            throw AppResultException.Status409Conflict("存在相同编码的菜单");
        }

        var menu = await _context.Set<SysMenu>()
            .Where(m => model.Id == m.Id)
            .FirstOrDefaultAsync();

        if (menu == null)
        {
            throw AppResultException.Status404NotFound("找不到菜单，更新失败");
        }


        MapperHelper.Map<MenuModel, SysMenu>(model, menu);
        _context.Update(menu);
        int ret = await _context.SaveChangesAsync();

        // 清空缓存
        await _cacheService.ClearMenuAndPermissionCacheAsync();

        if (ret == 0)
        {
            throw AppResultException.Status200OK("更新记录数为0");
        }

        return ret;
    }

    public async Task<int> DeleteAsync(Guid id)
    {
        var menus = await _context.Set<SysMenu>()
            .Where(m => m.Id == id)
            .ToListAsync();

        // 先更新数据库
        _context.RemoveRange(menus);
        int ret = await _context.SaveChangesAsync();

        // 再清空缓存
        await _cacheService.ClearMenuAndPermissionCacheAsync();

        return ret;
    }
}
