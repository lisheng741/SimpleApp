namespace Simple.Services;

public class OrganizationService
{
    private readonly ISimpleService _services;
    private readonly SimpleDbContext _context;

    public OrganizationService(SimpleDbContext context, ISimpleService services)
    {
        _context = context;
        _services = services;
    }

    public async Task<List<OrganizationModel>> GetAsync()
    {
        var organizations = await _context.Set<SysOrganization>().ToListAsync();
        return _services.Mapper.Map<List<OrganizationModel>>(organizations);
    }

    public async Task<PageResultModel<OrganizationModel>> GetPageAsync(OrganizationPageInputModel input)
    {
        var result = new PageResultModel<OrganizationModel>();
        var query = _context.Set<SysOrganization>().AsQueryable();

        // 根据条件查询
        if(input.ParentId.HasValue)
        {
            query = query.Where(o => o.ParentId == input.ParentId);
        }
        if (!string.IsNullOrEmpty(input.Name))
        {
            query = query.Where(o => o.Name.Contains(input.Name));
            query = query.Where(o => EF.Functions.Like(o.Name, $"%{input.Name}%"));
        }

        // 获取总数量
        result.TotalRows = await query.CountAsync();

        // 分页查询
        query = query.Page(input.PageNo, input.PageSize);
        var organizations = await query.ToListAsync();
        result.Rows = _services.Mapper.Map<List<OrganizationModel>>(organizations);

        result.SetPage(input);
        result.CountTotalPage();

        return result;
    }

    public async Task<List<AntTreeNode>> GetTreeAsync()
    {
        var organizations = await _context.Set<SysOrganization>().ToListAsync();
        List<TreeNode> nodes = _services.Mapper.Map<List<TreeNode>>(organizations);

        var builder = AntTreeNode.CreateBuilder(nodes);
        return builder.Build();
    }

    public async Task<AppResult> AddAsync(OrganizationModel model)
    {
        var organization = _services.Mapper.Map<SysOrganization>(model);
        await _context.AddAsync(organization);
        await _context.SaveChangesAsync();
        return AppResult.Status200OK("新增成功");
    }

    public async Task<AppResult> UpdateAsync(OrganizationModel model)
    {
        if(await _context.Set<SysOrganization>().AnyAsync(o => o.Id != model.Id && o.Code == model.Code))
        {
            return AppResult.Status409Conflict("存在相同编码的组织");
        }

        var organization = await _context.Set<SysOrganization>()
            .Where(o => model.Id == o.Id)
            .FirstOrDefaultAsync();

        if (organization == null)
        {
            return AppResult.Status404NotFound("找不到组织，更新失败");
        }

        organization.Name = model.Name;
        organization.Code = model.Code;
        organization.ParentId = model.Pid;
        organization.Sort = model.Sort;
        //organization.Remark = model.Remark;

        _context.Update(organization);
        int count = await _context.SaveChangesAsync();

        if(count == 0)
        {
            return AppResult.Status200OK("更新记录数为0");
        }

        return AppResult.Status200OK("更新成功");
    }

    public async Task<AppResult> DeleteAsync(List<OrganizationModel> models) // List<Guid> ids
    {
        var organizations = await _context.Set<SysOrganization>()
            .Where(o => models.Select(m => m.Id).Contains(o.Id))
            .ToListAsync();

        _context.RemoveRange(organizations);
        await _context.SaveChangesAsync();

        return AppResult.Status200OK("删除成功");
    }
}
