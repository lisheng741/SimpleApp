using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Simple.Common.Authentication.Jwt;
using Simple.Services.Account;

namespace Simple.Services.Account;

public class AccountService
{
    private readonly SimpleDbContext _context;

    public AccountService(SimpleDbContext context)
    {
        _context = context;
    }

    public Task<ApiResult> LoginAsync(LoginModel login)
    {
        //_context.Set<SysUserRole>().Include(t => t.Role);

        var jwtTokenModel = new JwtTokenModel(login.Account, "admin");
        string token = JwtHelper.Create(jwtTokenModel);

        return Task.FromResult(ApiResult.Status200OK("成功", token));
    }
}
