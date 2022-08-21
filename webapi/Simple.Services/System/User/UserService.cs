namespace Simple.Services;

public class UserService
{
    private readonly ISimpleService _services;
    private readonly SimpleDbContext _context;

    public UserService(SimpleDbContext context, ISimpleService services)
    {
        _context = context;
        _services = services;
    }

    public async Task<List<UserModel>> GetAsync()
    {
        var users = await _context.Set<SysUser>().ToListAsync();
        return _services.Mapper.Map<List<UserModel>>(users);
    }

    public async Task<PageResultModel<UserModel>> GetPageAsync(PageInputModel input)
    {
        var result = new PageResultModel<UserModel>();
        var query = _context.Set<SysUser>().AsQueryable();

        // 根据条件查询

        if (!string.IsNullOrEmpty(input.Name))
        {
            query = query.Where(p => EF.Functions.Like(p.Name!, $"%{input.Name}%"));
        }

        // 获取总数量
        result.TotalRows = await query.CountAsync();

        // 分页查询
        query = query.OrderBy(u => u.UserName).Page(input.PageNo, input.PageSize);
        var users = await query.ToListAsync();
        result.Rows = _services.Mapper.Map<List<UserModel>>(users);

        result.SetPage(input);
        result.CountTotalPage();

        return result;
    }

    public async Task<int> AddAsync(UserModel model)
    {
        //if (await _context.Set<SysUser>().AnyAsync(p => p.Id != model.Id && p.Code == model.Code))
        //{
        //    throw AppResultException.Status409Conflict("存在相同编码");
        //}

        var user = _services.Mapper.Map<SysUser>(model);
        await _context.AddAsync(user);
        return await _context.SaveChangesAsync();
    }

    public async Task<int> UpdateAsync(UserModel model)
    {
        //if (await _context.Set<SysUser>().AnyAsync(p => p.Id != model.Id && p.Code == model.Code))
        //{
        //    throw AppResultException.Status409Conflict("存在相同编码");
        //}

        var user = await _context.Set<SysUser>()
            .Where(p => model.Id == p.Id)
            .FirstOrDefaultAsync();

        if (user == null)
        {
            throw AppResultException.Status404NotFound("找不到角色，更新失败");
        }

        _services.Mapper.Map<UserModel, SysUser>(model, user);
        _context.Update(user);
        int ret = await _context.SaveChangesAsync();

        if (ret == 0)
        {
            throw AppResultException.Status200OK("更新记录数为0");
        }

        return ret;
    }

    public async Task<int> DeleteAsync(IEnumerable<Guid> ids)
    {
        var users = await _context.Set<SysUser>()
            .Where(p => ids.Contains(p.Id))
            .ToListAsync();

        _context.RemoveRange(users);
        return await _context.SaveChangesAsync();
    }


}
