using AutoMapper;

namespace SimpleApp.Services.Contracts;

public class TestProfile : Profile
{
    public TestProfile()
    {
        CreateMap<TestModel, TestDto>();
        CreateMap<TestDto, TestModel>();
    }
}
