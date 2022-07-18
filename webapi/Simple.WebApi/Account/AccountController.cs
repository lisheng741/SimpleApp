using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Simple.Services.Account;

namespace Simple.WebApi.Account;

[Route("api/[controller]/[action]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly AccountService _accountService;

    public AccountController(AccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpPost]
    public Task<ApiResult<string>> Login([Required] LoginInput input)
        => _accountService.LoginAsync(input);
}
