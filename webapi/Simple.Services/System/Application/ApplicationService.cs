using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Services;

public class ApplicationService
{
    private readonly SimpleDbContext _context;
    private readonly MenuService _menuService;
    private readonly CacheService _cacheService;

    public ApplicationService(SimpleDbContext context, MenuService menuService, CacheService cacheService)
    {
        _context = context;
        _menuService = menuService;
        _cacheService = cacheService;
    }

    public async Task<List<ApplicationModel>> GetAsync()
    {
        var applications = await _context.Set<SysApplication>().ToListAsync();
        return MapperHelper.Map<List<ApplicationModel>>(applications);
    }

    public async Task<List<ApplicationModel>> GetAsync(params string[] codes)
    {
        var applications = await _context.Set<SysApplication>().Where(a => codes.Contains(a.Code)).ToListAsync();
        return MapperHelper.Map<List<ApplicationModel>>(applications);
    }

    public async Task<PageResultModel<ApplicationModel>> GetPageAsync(PageInputModel input)
    {
        var result = new PageResultModel<ApplicationModel>();
        var query = _context.Set<SysApplication>().AsQueryable();

        // 根据条件查询
        if (!string.IsNullOrEmpty(input.Code))
        {
            query = query.Where(a => EF.Functions.Like(a.Code, $"%{input.Code}%"));
        }
        if (!string.IsNullOrEmpty(input.Name))
        {
            query = query.Where(a => EF.Functions.Like(a.Name, $"%{input.Name}%"));
        }

        // 获取总数量
        result.TotalRows = await query.CountAsync();

        // 分页查询
        query = query.OrderBy(a => a.Sort).Page(input.PageNo, input.PageSize);
        var applications = await query.ToListAsync();
        result.Rows = MapperHelper.Map<List<ApplicationModel>>(applications);

        result.SetPage(input);
        result.CountTotalPage();

        return result;
    }

    public async Task<int> AddAsync(ApplicationModel model)
    {
        if (await _context.Set<SysApplication>().AnyAsync(a => a.Id != model.Id && a.Code == model.Code))
        {
            throw AppResultException.Status409Conflict("存在相同编码");
        }

        var application = MapperHelper.Map<SysApplication>(model);
        await _context.AddAsync(application);
        return await _context.SaveChangesAsync();
    }

    public async Task<int> UpdateAsync(ApplicationModel model)
    {
        if (await _context.Set<SysApplication>().AnyAsync(a => a.Id != model.Id && a.Code == model.Code))
        {
            throw AppResultException.Status409Conflict("存在相同编码");
        }

        var application = await _context.Set<SysApplication>()
            .Where(a => model.Id == a.Id)
            .FirstOrDefaultAsync();

        if (application == null)
        {
            throw AppResultException.Status404NotFound("找不到应用，更新失败");
        }

        MapperHelper.Map<ApplicationModel, SysApplication>(model, application);
        _context.Update(application);
        int ret = await _context.SaveChangesAsync();

        // 清缓存
        await _cacheService.ClearApplicationCacheAsync();

        if (ret == 0)
        {
            throw AppResultException.Status200OK("更新记录数为0");
        }

        return ret;
    }

    public async Task<int> DeleteAsync(IEnumerable<Guid> ids)
    {
        var applications = await _context.Set<SysApplication>()
            .Where(a => ids.Contains(a.Id))
            .ToListAsync();

        _context.RemoveRange(applications);
        int ret = await _context.SaveChangesAsync();

        // 清缓存
        await _cacheService.ClearApplicationCacheAsync();

        return ret;
    }

    /// <summary>
    /// 设置默认应用
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<int> SetDefault(Guid id)
    {
        var applications = await _context.Set<SysApplication>().ToListAsync();
        if (!applications.Any(a => a.Id == id))
        {
            throw AppResultException.Status404NotFound("找不到该应用，设置失败");
        }

        foreach(var application in applications)
        {
            if(application.Id == id)
            {
                application.IsActive = true;
            }
            else
            {
                application.IsActive = false;
            }
        }
        _context.UpdateRange(applications);
        int ret = await _context.SaveChangesAsync();

        // 清缓存
        await _cacheService.ClearApplicationCacheAsync();

        return ret;
    }
}
