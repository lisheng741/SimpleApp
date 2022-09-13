using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Simple.Common.EventBus;
using Simple.Common.Filters;

namespace Simple.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [DisabledRequestRecord]
    public class TestController : ControllerBase
    {
        private readonly TestService _testService;
        private readonly IMapper _mapper;
        private readonly IEventPublisher _eventPublisher;

        public TestController(TestService testService, IMapper mapper, IEnumerable<ITestService> testServices, ITestService test, IEventPublisher eventPublisher)
        {
            _testService = testService;
            _mapper = mapper;
            _eventPublisher = eventPublisher;
        }

        /// <summary>
        /// 测试获取
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string Get()
        {
            //var @event = new TestEventModel();
            //_eventPublisher.PublishAsync(@event).GetAwaiter().GetResult();

            return _testService.Get();
        }

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
        public string Throw2(string s = "12334")
        {
            throw new Exception("测试异常");
        }
    }
}
