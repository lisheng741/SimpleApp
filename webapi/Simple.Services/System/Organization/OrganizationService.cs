namespace Simple.Services.System;

public class OrganizationService
{
    private readonly ISimpleService _services;
    private readonly SimpleDbContext _context;
    private readonly DbSet<SysOrganization> _organization;

    public OrganizationService(SimpleDbContext context, ISimpleService services)
    {
        _context = context;
        _services = services;

        _organization = context.Set<SysOrganization>();
    }

    public List<OrganizationModel> Get()
    {
        var organizations = _context.Set<SysOrganization>().ToList();
        return _services.Mapper.Map<List<OrganizationModel>>(organizations);
    }

    public int Add(List<OrganizationModel> models)
    {
        var organizations = _services.Mapper.Map<List<SysOrganization>>(models);
        _context.Add(organizations);
        return _context.SaveChanges();
    }

    public int Update(List<OrganizationModel> models)
    {
        var organizations = _context.Set<SysOrganization>()
            .Where(o => models.Select(m => m.Id).Contains(o.Id))
            .ToList();
        _context.UpdateRange(organizations);
        return _context.SaveChanges();
    }

    public int Delete(List<Guid> ids)
    {
        var organizations = _context.Set<SysOrganization>().Where(o => ids.Contains(o.Id)).ToList();
        _context.RemoveRange(organizations);
        return _context.SaveChanges();
    }
}
