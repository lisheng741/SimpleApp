using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace Simple.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly TestService _testService;
        private readonly IMapper _mapper;

        public TestController(TestService testService, IMapper mapper, IEnumerable<ITestService> testServices, ITestService test)
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
            => _testService.Get();

        [HttpGet]
        public string Throw()
        {
            throw new AppResultException(new AppResult(401, "测试401返回"));
            throw new Exception("测试异常");
        }

        [HttpGet]
        public string Throw2()
        {
            throw new Exception("测试异常");
        }
    }
}
