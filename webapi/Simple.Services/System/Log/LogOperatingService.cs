namespace Simple.Services;

public class LogOperatingService
{
    private readonly SimpleDbContext _context;

    public LogOperatingService(SimpleDbContext context)
    {
        _context = context;
    }

    public async Task<PageResultModel<LogOperatingModel>> GetPageAsync(LogPageInputModel input)
    {
        var result = new PageResultModel<LogOperatingModel>();
        var query = _context.Set<SysLogOperating>().AsQueryable();

        // 根据条件查询
        if (!string.IsNullOrEmpty(input.Name))
        {
            query = query.Where(l => EF.Functions.Like(l.Name!, $"%{input.Name}%"));
        }
        if (!string.IsNullOrEmpty(input.Success))
        {
            bool isSuccess = input.Success == "Y";
            query = query.Where(l => l.IsSuccess == isSuccess);
        }
        if (input.SearchBeginTime.HasValue)
        {
            query = query.Where(l => l.OperatingTime >= input.SearchBeginTime);
        }
        if (input.SearchEndTime.HasValue)
        {
            query = query.Where(l => l.OperatingTime <= input.SearchEndTime);
        }

        // 获取总数量
        result.TotalRows = await query.CountAsync();

        // 分页查询
        query = query.OrderByDescending(l => l.Id).Page(input.PageNo, input.PageSize);
        var logs = await query.ToListAsync();
        result.Rows = MapperHelper.Map<List<LogOperatingModel>>(logs);

        result.SetPage(input);
        result.CountTotalPage();

        return result;
    }
}
