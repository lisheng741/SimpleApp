namespace Simple.Services;

public class DictionaryService
{
    private readonly SimpleDbContext _context;

    public DictionaryService(SimpleDbContext context)
    {
        _context = context;
    }

    public async Task<List<DictionaryModel>> GetAsync()
    {
        var dictionaries = await _context.Set<SysDictionary>().ToListAsync();
        return MapperHelper.Map<List<DictionaryModel>>(dictionaries);
    }

    public async Task<PageResultModel<DictionaryModel>> GetPageAsync(PageInputModel input)
    {
        var result = new PageResultModel<DictionaryModel>();
        var query = _context.Set<SysDictionary>().AsQueryable();

        // 根据条件查询
        if (!string.IsNullOrEmpty(input.Code))
        {
            query = query.Where(d => EF.Functions.Like(d.Code, $"%{input.Code}%"));
        }
        if (!string.IsNullOrEmpty(input.Name))
        {
            query = query.Where(d => EF.Functions.Like(d.Name, $"%{input.Name}%"));
        }

        // 获取总数量
        result.TotalRows = await query.CountAsync();

        // 分页查询
        query = query.OrderBy(d => d.Sort).Page(input.PageNo, input.PageSize);
        var dictionaries = await query.ToListAsync();
        result.Rows = MapperHelper.Map<List<DictionaryModel>>(dictionaries);

        result.SetPage(input);
        result.CountTotalPage();

        return result;
    }

    public async Task<List<DictionaryTreeModel>> GetTreeAsync()
    {
        var dictionaries = await _context.Set<SysDictionary>()
            .Include(d => d.DictionaryItems)
            .ToListAsync();

        return MapperHelper.Map<List<DictionaryTreeModel>>(dictionaries);
    }

    public async Task<int> AddAsync(DictionaryModel model)
    {
        if (await _context.Set<SysDictionary>().AnyAsync(d => d.Id != model.Id && d.Code == model.Code))
        {
            throw AppResultException.Status409Conflict("存在相同编码");
        }

        var dictionary = MapperHelper.Map<SysDictionary>(model);
        await _context.AddAsync(dictionary);
        return await _context.SaveChangesAsync();
    }

    public async Task<int> UpdateAsync(DictionaryModel model)
    {
        if (await _context.Set<SysDictionary>().AnyAsync(d => d.Id != model.Id && d.Code == model.Code))
        {
            throw AppResultException.Status409Conflict("存在相同编码");
        }

        var dictionary = await _context.Set<SysDictionary>()
            .Where(d => model.Id == d.Id)
            .FirstOrDefaultAsync();

        if (dictionary == null)
        {
            throw AppResultException.Status404NotFound("找不到角色，更新失败");
        }

        MapperHelper.Map<DictionaryModel, SysDictionary>(model, dictionary);
        _context.Update(dictionary);
        int ret = await _context.SaveChangesAsync();

        if (ret == 0)
        {
            throw AppResultException.Status200OK("更新记录数为0");
        }

        return ret;
    }

    public async Task<int> DeleteAsync(IEnumerable<Guid> ids)
    {
        var dictionaries = await _context.Set<SysDictionary>()
            .Where(d => ids.Contains(d.Id))
            .ToListAsync();

        _context.RemoveRange(dictionaries);
        return await _context.SaveChangesAsync();
    }

    public async Task<List<DictionaryItemModel>> GetItemsAsync(string code)
    {
        var items = await _context.Set<SysDictionary>()
            .Include(d => d.DictionaryItems)
            .Where(d => d.Code == code)
            .FirstOrDefaultAsync();

        if (items == null)
        {
            return new List<DictionaryItemModel>();
        }

        return MapperHelper.Map<List<DictionaryItemModel>>(items.DictionaryItems);
    }
}
