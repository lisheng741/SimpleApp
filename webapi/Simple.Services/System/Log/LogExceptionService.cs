namespace Simple.Services;

public class LogExceptionService
{
    private readonly SimpleDbContext _context;

    public LogExceptionService(SimpleDbContext context)
    {
        _context = context;
    }

    public async Task<PageResultModel<LogExceptionModel>> GetPageAsync(LogPageInputModel input)
    {
        var result = new PageResultModel<LogExceptionModel>();
        var query = _context.Set<SysLogException>().AsQueryable();

        // 根据条件查询
        if (input.EventId.HasValue)
        {
            query = query.Where(l => l.EventId == input.EventId);
        }
        if (!string.IsNullOrEmpty(input.Name))
        {
            query = query.Where(l => EF.Functions.Like(l.Name!, $"%{input.Name}%"));
        }
        if (input.SearchBeginTime.HasValue)
        {
            query = query.Where(l => l.ExceptionTime >= input.SearchBeginTime);
        }
        if (input.SearchEndTime.HasValue)
        {
            query = query.Where(l => l.ExceptionTime <= input.SearchEndTime);
        }

        // 获取总数量
        result.TotalRows = await query.CountAsync();

        // 分页查询
        query = query.OrderByDescending(l => l.Id).Page(input.PageNo, input.PageSize);
        var logs = await query.ToListAsync();
        result.Rows = MapperHelper.Map<List<LogExceptionModel>>(logs);

        result.SetPage(input);
        result.CountTotalPage();

        return result;
    }
}
