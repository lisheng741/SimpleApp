namespace Simple.Services;

public class UserService
{
    private readonly SimpleDbContext _context;

    public UserService(SimpleDbContext context)
    {
        _context = context;
    }

    public async Task<List<UserModel>> GetAsync()
    {
        var users = await _context.Set<SysUser>().ToListAsync();
        return MapperHelper.Map<List<UserModel>>(users);
    }

    public async Task<PageResultModel<UserModel>> GetPageAsync(UserPageInputModel input)
    {
        var result = new PageResultModel<UserModel>();
        var query = _context.Set<SysUser>().AsQueryable();

        // 根据条件查询
        if (input.OrganizationId.HasValue)
        {
            query = query.Where(u => u.OrganizationId == input.OrganizationId);
        }
        if (!string.IsNullOrEmpty(input.SearchValue))
        {
            query = query.Where(u => EF.Functions.Like(u.UserName, $"%{input.SearchValue}%") ||
                                     EF.Functions.Like(u.Name!, $"%{input.SearchValue}%"));
        }
        if (input.SearchStatus.HasValue)
        {
            query = query.Where(u => u.IsEnabled == (input.SearchStatus == 1));
        }

        // 获取总数量
        result.TotalRows = await query.CountAsync();

        // 分页查询
        query = query.OrderBy(u => u.UserName).Page(input.PageNo, input.PageSize);
        var users = await query.ToListAsync();
        result.Rows = MapperHelper.Map<List<UserModel>>(users);

        result.SetPage(input);
        result.CountTotalPage();

        return result;
    }

    public async Task<int> AddAsync(UserModel model)
    {
        if (await _context.Set<SysUser>().AnyAsync(u => u.UserName == model.Account))
        {
            throw AppResultException.Status409Conflict("存在相同用户名");
        }

        var user = MapperHelper.Map<SysUser>(model);
        user.SetPassword(model.Password);

        await _context.AddAsync(user);
        return await _context.SaveChangesAsync();
    }

    public async Task<int> UpdateAsync(UserModel model)
    {
        if (await _context.Set<SysUser>().AnyAsync(u => u.Id != model.Id && u.UserName == model.Account))
        {
            throw AppResultException.Status409Conflict("存在相同用户名");
        }

        var user = await _context.Set<SysUser>()
            .Where(u => model.Id == u.Id)
            .FirstOrDefaultAsync();

        if (user == null)
        {
            throw AppResultException.Status404NotFound("找不到用户，更新失败");
        }

        MapperHelper.Map<UserModel, SysUser>(model, user);
        user.SetPassword(model.Password);

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
            .Where(u => ids.Contains(u.Id))
            .ToListAsync();

        _context.RemoveRange(users);
        return await _context.SaveChangesAsync();
    }

    /// <summary>
    /// 修改状态（IsEnabled）
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="isEnabled"></param>
    /// <returns></returns>
    public async Task<int> SetEnabledAsync(Guid userId, bool isEnabled)
    {
        // 查找用户
        var user = await _context.Set<SysUser>()
            .Where(u => u.Id == userId)
            .FirstOrDefaultAsync();

        if (user == null)
        {
            throw AppResultException.Status404NotFound("找不到用户，更新失败");
        }

        // 更新状态
        user.IsEnabled = isEnabled;

        _context.Update(user);
        return await _context.SaveChangesAsync();
    }

    public async Task<UserModel> GetUserInfoAsync(Guid id)
    {
        var user = await _context.Set<SysUser>()
            .Where(u => u.Id == id)
            .FirstOrDefaultAsync();

        if (user == null)
        {
            return new UserModel();
        }

        return MapperHelper.Map<UserModel>(user);
    }

    public async Task<int> ResetPassword(Guid id, string password = "123456")
    {
        var user = await _context.Set<SysUser>()
            .Where(u => u.Id == id)
            .FirstOrDefaultAsync();

        if (user == null)
        {
            throw AppResultException.Status404NotFound("找不到用户，重置失败");
        }

        user.SetPassword(password);
        _context.Update(user);
        return await _context.SaveChangesAsync();
    }
}
