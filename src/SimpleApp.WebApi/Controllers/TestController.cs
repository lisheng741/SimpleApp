using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleApp.Services.Contracts;

namespace SimpleApp.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ITestService _testService;

        public TestController(ITestService testService)
        {
            _testService = testService;
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
    }
}
