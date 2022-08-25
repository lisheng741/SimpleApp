namespace Simple.Services;

public class OrganizationService
{
    private readonly SimpleDbContext _context;

    public OrganizationService(SimpleDbContext context)
    {
        _context = context;
    }

    public async Task<List<OrganizationModel>> GetAsync()
    {
        var organizations = await _context.Set<SysOrganization>().ToListAsync();
        return MapperHelper.Map<List<OrganizationModel>>(organizations);
    }

    public async Task<PageResultModel<OrganizationModel>> GetPageAsync(OrganizationPageInputModel input)
    {
        var result = new PageResultModel<OrganizationModel>();
        var query = _context.Set<SysOrganization>().AsQueryable();

        // 根据条件查询
        if (input.Pid.HasValue)
        {
            query = query.Where(o => o.Id == input.Pid || o.ParentId == input.Pid);
        }
        if (!string.IsNullOrEmpty(input.Name))
        {
            // AND ((@__input_Name_0 LIKE N'') OR (CHARINDEX(@__input_Name_0, [s].[Name]) > 0))
            //query = query.Where(o => o.Name.Contains(input.Name));

            // AND ([s].[Name] LIKE @__Format_1)
            query = query.Where(o => EF.Functions.Like(o.Name, $"%{input.Name}%"));
        }

        // 获取总数量
        result.TotalRows = await query.CountAsync();

        // 分页查询
        query = query.OrderBy(o => o.Sort).Page(input.PageNo, input.PageSize);
        var organizations = await query.ToListAsync();
        result.Rows = MapperHelper.Map<List<OrganizationModel>>(organizations);

        result.SetPage(input);
        result.CountTotalPage();

        return result;
    }

    public async Task<List<AntTreeNode>> GetTreeAsync()
    {
        var organizations = await _context.Set<SysOrganization>().ToListAsync();
        List<TreeNode> nodes = MapperHelper.Map<List<TreeNode>>(organizations);

        var builder = AntTreeNode.CreateBuilder(nodes);
        return builder.Build();
    }

    public async Task<int> AddAsync(OrganizationModel model)
    {
        if (await _context.Set<SysOrganization>().AnyAsync(o => o.Code == model.Code))
        {
            throw AppResultException.Status409Conflict("存在相同编码的组织");
        }

        var organization = MapperHelper.Map<SysOrganization>(model);
        await _context.AddAsync(organization);
        return await _context.SaveChangesAsync();
    }

    public async Task<int> UpdateAsync(OrganizationModel model)
    {
        if (await _context.Set<SysOrganization>().AnyAsync(o => o.Id != model.Id && o.Code == model.Code))
        {
            throw AppResultException.Status409Conflict("存在相同编码的组织");
        }

        var organization = await _context.Set<SysOrganization>()
            .Where(o => model.Id == o.Id)
            .FirstOrDefaultAsync();

        if (organization == null)
        {
            throw AppResultException.Status404NotFound("找不到组织，更新失败");
        }

        MapperHelper.Map<OrganizationModel, SysOrganization>(model, organization);
        _context.Update(organization);
        int ret = await _context.SaveChangesAsync();

        if (ret == 0)
        {
            throw AppResultException.Status200OK("更新记录数为0");
        }

        return ret;
    }

    public async Task<int> DeleteAsync(IEnumerable<Guid> ids) // List<Guid> ids
    {
        var organizations = await _context.Set<SysOrganization>()
            .Where(o => ids.Contains(o.Id))
            .ToListAsync();

        _context.RemoveRange(organizations);
        return await _context.SaveChangesAsync();
    }
}
