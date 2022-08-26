using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Service;

public class UserDataScopeService
{
    private readonly SimpleDbContext _context;

    public UserDataScopeService(SimpleDbContext context)
    {
        _context = context;
    }

    public async Task<List<Guid>> GetDataScopeAsync(params Guid[] userIds)
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
}
