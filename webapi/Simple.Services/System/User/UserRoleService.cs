namespace Simple.Services;

public class UserRoleService
{
    private readonly SimpleDbContext _context;

    public UserRoleService(SimpleDbContext context)
    {
        _context = context;
    }

    public async Task<List<Guid>> GetUserRoleIdsAsync(params Guid[] userIds)
    {
        var roleIds = await _context.Set<SysUser>()
            .Include(u => u.UserRoles)
            .Where(u => userIds.Contains(u.Id))
            .SelectMany(u => u.UserRoles.Select(rm => rm.RoleId))
            .ToListAsync();

        return roleIds;
    }

    public async Task<int> SetUserRoleAsync(Guid userId, List<Guid> roleIds)
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

        // 删除
        foreach (var userRole in user.UserRoles)
        {
            if (roleIds.Any(item => item == userRole.RoleId))
            {
                continue;
            }
            _context.Remove(userRole);
        }

        // 新增
        // 取差集
        List<Guid> exceptRoleIds = roleIds
            .Except(user.UserRoles.Select(ur => ur.RoleId))
            .ToList();
        List<SysUserRole> userRoles = new List<SysUserRole>();
        foreach (var roleId in exceptRoleIds)
        {
            var userRole = new SysUserRole()
            {
                UserId = user.Id,
                RoleId = roleId
            };
            userRoles.Add(userRole);
        }
        user.UserRoles.AddRange(userRoles);

        _context.UpdateRange(user);
        return await _context.SaveChangesAsync();
    }
}
