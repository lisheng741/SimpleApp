using System.ComponentModel.DataAnnotations;
using Simple.Services.Account;

namespace Simple.WebApi.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly AccountService _accountService;

    public AccountController(AccountService accountService)
    {
        _accountService = accountService;
    }

    /// <summary>
    /// 登录
    /// </summary>
    /// <param name="login"></param>
    /// <returns></returns>
    [HttpPost]
    public Task<ApiResult> Login([Required] LoginModel login)
        => _accountService.LoginAsync(login);
}
