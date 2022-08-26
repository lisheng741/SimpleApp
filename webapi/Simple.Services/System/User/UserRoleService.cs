namespace Simple.Services;

public class UserRoleService
{
    private readonly SimpleDbContext _context;

    public UserRoleService(SimpleDbContext context)
    {
        _context = context;
    }

    public async Task<List<Guid>> GetRoleAsync(params Guid[] userIds)
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
}
