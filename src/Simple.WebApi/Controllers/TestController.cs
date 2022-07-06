using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Simple.Services;

namespace SimpleApp.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly TestService _testService;
        private readonly IMapper _mapper;

        public TestController(TestService testService, IMapper mapper)
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
    }
}
