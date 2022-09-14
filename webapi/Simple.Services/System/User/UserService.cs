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

    public async Task<int> UpdatePasswordAsync(Guid id, string password, string newPassword = "123456")
    {
        string passwordHash = HashHelper.Md5(password);

        var user = await _context.Set<SysUser>()
            .Where(u => u.Id == id)
            .Where(u => u.Password == passwordHash)
            .FirstOrDefaultAsync();

        if (user == null)
        {
            throw AppResultException.Status404NotFound("用户不存在或密码不匹配");
        }

        user.SetPassword(newPassword);
        _context.Update(user);
        return await _context.SaveChangesAsync();
    }

    public async Task<int> SetPasswordAsync(Guid id, string password = "123456")
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

    public async Task<List<Guid>> GetRoleIdsAsync(params Guid[] userIds)
    {
        var roleIds = await _context.Set<SysUser>()
            .Include(u => u.UserRoles)
            .Where(u => userIds.Contains(u.Id))
            .SelectMany(u => u.UserRoles.Select(rm => rm.RoleId))
            .ToListAsync();

        return roleIds;
    }

    public async Task<int> SetRoleAsync(Guid userId, Guid[] roleIds)
    {
        // 查找用户
        var user = await _context.Set<SysUser>()
            .Include(u => u.UserRoles)
            .Where(u => u.Id == userId)
            .FirstOrDefaultAsync();

        if (user == null)
        {
            throw AppResultException.Status404NotFound("找不到用户，设置失败");
        }

        user.SetRole(roleIds);

        _context.UpdateRange(user);
        return await _context.SaveChangesAsync();
    }

    public async Task<List<Guid>> GetDataScopesAsync(params Guid[] userIds)
    {
        var organizationIds = await _context.Set<SysUser>()
            .Include(u => u.UserDataScopes)
            .Where(u => userIds.Contains(u.Id))
            .SelectMany(u => u.UserDataScopes.Select(ud => ud.OrganizationId))
            .ToListAsync();

        return organizationIds;
    }

    public async Task<int> SetDataScopeAsync(Guid userId, Guid[] organizationIds)
    {
        // 查找用户
        var user = await _context.Set<SysUser>()
            .Include(u => u.UserDataScopes)
            .Where(u => u.Id == userId)
            .FirstOrDefaultAsync();

        if (user == null)
        {
            throw AppResultException.Status404NotFound("找不到用户，设置失败");
        }

        user.SetDataScope(organizationIds);

        _context.UpdateRange(user);
        return await _context.SaveChangesAsync();
    }

    /// <summary>
    /// 获取用户拥有的菜单的应用编码列表
    /// </summary>
    /// <returns></returns>
    public async Task<List<string>> GetApplicationCodeListAsync(Guid userId)
    {
        var user = await _context.Set<SysUser>()
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .ThenInclude(r => r!.RoleMenus)
            .ThenInclude(rm => rm.Menu)
            .Where(u => u.Id == userId).Where(u => u.IsEnabled)
            .FirstOrDefaultAsync();

        if(user == null)
        {
            return new List<string>();
        }

        //var applicationCodes = user.GetApplicationCodes();
        var applicationCodes = user.UserRoles
            .Where(ur => ur.Role != null).Select(ur => ur.Role!)
            .SelectMany(r => r!.RoleMenus)
            .Where(rm => rm.Menu != null).Select(rm => rm.Menu!)
            .Where(m => m.Application != null).Select(m => m!.Application!).ToList();

        return applicationCodes;
    }
}
