using AutoMapper;

namespace Simple.Services;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        // System
        CreateMap<SysOrganization, OrganizationModel>()
            .ForMember(d => d.Id, options => options.Ignore())
            .ForMember(d => d.Pid, options => options.MapFrom(s => s.ParentId));
        CreateMap<OrganizationModel, SysOrganization>()
            .ForMember(d => d.Id, options => options.Ignore())
            .ForMember(d => d.ParentId, options => options.MapFrom(s => s.Pid));
        CreateMap<SysOrganization, TreeNode>();
    }
}
