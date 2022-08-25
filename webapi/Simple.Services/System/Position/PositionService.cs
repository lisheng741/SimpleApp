namespace Simple.Services;

public class PositionService
{
    private readonly SimpleDbContext _context;

    public PositionService(SimpleDbContext context)
    {
        _context = context;
    }

    public async Task<List<PositionModel>> GetAsync()
    {
        var positions = await _context.Set<SysPosition>().ToListAsync();
        return MapperHelper.Map<List<PositionModel>>(positions);
    }

    public async Task<PageResultModel<PositionModel>> GetPageAsync(PageInputModel input)
    {
        var result = new PageResultModel<PositionModel>();
        var query = _context.Set<SysPosition>().AsQueryable();

        // 根据条件查询
        if (!string.IsNullOrEmpty(input.Code))
        {
            query = query.Where(p => EF.Functions.Like(p.Code, $"%{input.Code}%"));
        }
        if (!string.IsNullOrEmpty(input.Name))
        {
            query = query.Where(p => EF.Functions.Like(p.Name, $"%{input.Name}%"));
        }

        // 获取总数量
        result.TotalRows = await query.CountAsync();

        // 分页查询
        query = query.OrderBy(p => p.Sort).Page(input.PageNo, input.PageSize);
        var positions = await query.ToListAsync();
        result.Rows = MapperHelper.Map<List<PositionModel>>(positions);

        result.SetPage(input);
        result.CountTotalPage();

        return result;
    }

    public async Task<int> AddAsync(PositionModel model)
    {
        if (await _context.Set<SysPosition>().AnyAsync(p => p.Id != model.Id && p.Code == model.Code))
        {
            throw AppResultException.Status409Conflict("存在相同编码的岗位");
        }

        var position = MapperHelper.Map<SysPosition>(model);
        await _context.AddAsync(position);
        return await _context.SaveChangesAsync();
    }

    public async Task<int> UpdateAsync(PositionModel model)
    {
        if (await _context.Set<SysPosition>().AnyAsync(p => p.Id != model.Id && p.Code == model.Code))
        {
            throw AppResultException.Status409Conflict("存在相同编码的岗位");
        }

        var position = await _context.Set<SysPosition>()
            .Where(p => model.Id == p.Id)
            .FirstOrDefaultAsync();

        if (position == null)
        {
            throw AppResultException.Status404NotFound("找不到岗位，更新失败");
        }

        MapperHelper.Map<PositionModel, SysPosition>(model, position);
        _context.Update(position);
        int ret = await _context.SaveChangesAsync();

        if (ret == 0)
        {
            throw AppResultException.Status200OK("更新记录数为0");
        }

        return ret;
    }

    public async Task<int> DeleteAsync(IEnumerable<Guid> ids)
    {
        var positions = await _context.Set<SysPosition>()
            .Where(p => ids.Contains(p.Id))
            .ToListAsync();

        _context.RemoveRange(positions);
        return await _context.SaveChangesAsync();
    }
}
