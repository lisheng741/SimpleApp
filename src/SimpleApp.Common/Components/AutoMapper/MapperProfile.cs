using AutoMapper;

namespace SimpleApp.Common.Components.AutoMapper;

public abstract class MapperProfile : Profile
{
    public MapperProfile()
    {
        //驼峰命名与Pascal命名的兼容
        DestinationMemberNamingConvention = new PascalCaseNamingConvention();
        SourceMemberNamingConvention = new LowerUnderscoreNamingConvention();
    }
}
