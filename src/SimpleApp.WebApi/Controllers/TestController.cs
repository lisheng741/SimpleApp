using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleApp.Model;
using SimpleApp.Services.Contracts;

namespace SimpleApp.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ITestService _testService;
        private readonly IMapper _mapper;

        public TestController(ITestService testService, IMapper mapper)
        {
            _testService = testService;
            _mapper = mapper;
        }

        [HttpGet]
        public string Get()
            => _testService.Get();

        [HttpGet]
        public string GetToken()
            => _testService.GetToken();

        [Authorize]
        [HttpGet]
        public string Auth()
            => "auth";

        [HttpGet]
        public TestDto GetDto(string name)
        {
            TestModel testModel = new TestModel(name);
            return _mapper.Map<TestModel, TestDto>(testModel);
        }
    }
}
