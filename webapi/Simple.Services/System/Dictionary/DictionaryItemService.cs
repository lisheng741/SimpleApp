namespace Simple.Services;

public class DictionaryItemService
{
    private readonly SimpleDbContext _context;

    public DictionaryItemService(SimpleDbContext context)
    {
        _context = context;
    }

    public async Task<List<DictionaryItemModel>> GetAsync()
    {
        var dictionaryItems = await _context.Set<SysDictionaryItem>().ToListAsync();
        return MapperHelper.Map<List<DictionaryItemModel>>(dictionaryItems);
    }

    public async Task<PageResultModel<DictionaryItemModel>> GetPageAsync(DictionaryItemPageInputModel input)
    {
        var result = new PageResultModel<DictionaryItemModel>();
        var query = _context.Set<SysDictionaryItem>().AsQueryable();

        // 根据条件查询
        if (input.TypeId.HasValue)
        {
            query = query.Where(di => di.DictionaryId == input.TypeId); 
        }
        if (!string.IsNullOrEmpty(input.Code))
        {
            query = query.Where(di => EF.Functions.Like(di.Code, $"%{input.Code}%"));
        }
        if (!string.IsNullOrEmpty(input.Value))
        {
            query = query.Where(di => EF.Functions.Like(di.Name, $"%{input.Value}%"));
        }

        // 获取总数量
        result.TotalRows = await query.CountAsync();

        // 分页查询
        query = query.OrderBy(di => di.Sort).Page(input.PageNo, input.PageSize);
        var dictionaryItems = await query.ToListAsync();
        result.Rows = MapperHelper.Map<List<DictionaryItemModel>>(dictionaryItems);

        result.SetPage(input);
        result.CountTotalPage();

        return result;
    }

    public async Task<int> AddAsync(DictionaryItemModel model)
    {
        if (await _context.Set<SysDictionaryItem>()
            .AnyAsync(di => di.Id != model.Id && di.DictionaryId == model.TypeId && di.Code == model.Code))
        {
            throw AppResultException.Status409Conflict("存在相同编码");
        }

        var dictionaryItem = MapperHelper.Map<SysDictionaryItem>(model);
        await _context.AddAsync(dictionaryItem);
        return await _context.SaveChangesAsync();
    }

    public async Task<int> UpdateAsync(DictionaryItemModel model)
    {
        if (await _context.Set<SysDictionaryItem>()
            .AnyAsync(di => di.Id != model.Id && di.DictionaryId == model.TypeId && di.Code == model.Code))
        {
            throw AppResultException.Status409Conflict("存在相同编码");
        }

        var dictionaryItem = await _context.Set<SysDictionaryItem>()
            .Where(di => model.Id == di.Id)
            .FirstOrDefaultAsync();

        if (dictionaryItem == null)
        {
            throw AppResultException.Status404NotFound("找不到字典子项，更新失败");
        }

        MapperHelper.Map<DictionaryItemModel, SysDictionaryItem>(model, dictionaryItem);
        _context.Update(dictionaryItem);
        int ret = await _context.SaveChangesAsync();

        if (ret == 0)
        {
            throw AppResultException.Status200OK("更新记录数为0");
        }

        return ret;
    }

    public async Task<int> DeleteAsync(Guid id)
    {
        var dictionaryItems = await _context.Set<SysDictionaryItem>()
            .Where(di => di.Id == id)
            .ToListAsync();

        _context.RemoveRange(dictionaryItems);
        return await _context.SaveChangesAsync();
    }
}
