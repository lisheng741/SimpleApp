using AutoMapper;

namespace Simple.Services;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        // System
        CreateMap<SysOrganization, OrganizationModel>();
        CreateMap<OrganizationModel, SysOrganization>();
    }
}
