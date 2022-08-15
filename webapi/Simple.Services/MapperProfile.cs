using AutoMapper;

namespace Simple.Services;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        // System
        // Organization
        CreateMap<SysOrganization, TreeNode>();

        // 专有配置
        this.ConfigureMapper();
    }
}
