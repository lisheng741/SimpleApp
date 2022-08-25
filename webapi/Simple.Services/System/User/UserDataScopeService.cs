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

    public async Task<List<Guid>> GetUserDataScopeIdsAsync(params Guid[] userIds)
    {
        var organizationIds = await _context.Set<SysUser>()
            .Include(u => u.UserDataScopes)
            .Where(u => userIds.Contains(u.Id))
            .SelectMany(u => u.UserDataScopes.Select(ud => ud.OrganizationId))
            .ToListAsync();

        return organizationIds;
    }

    public async Task<int> SetUserDataScopeAsync(Guid userId, List<Guid> organizationIds)
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

        // 删除
        foreach (var userDataScope in user.UserDataScopes)
        {
            if (organizationIds.Any(item => item == userDataScope.OrganizationId))
            {
                continue;
            }
            _context.Remove(userDataScope);
        }

        // 新增
        // 取差集
        List<Guid> exceptOrganizationIds = organizationIds
            .Except(user.UserDataScopes.Select(ud => ud.OrganizationId))
            .ToList();
        List<SysUserDataScope> userDataScopes = new List<SysUserDataScope>();
        foreach (var organizationId in exceptOrganizationIds)
        {
            var userDataScope = new SysUserDataScope()
            {
                UserId = user.Id,
                OrganizationId = organizationId
            };
            userDataScopes.Add(userDataScope);
        }
        user.UserDataScopes.AddRange(userDataScopes);

        _context.UpdateRange(user);
        return await _context.SaveChangesAsync();
    }
}
