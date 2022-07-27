using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Simple.Common.Authentication.Jwt;
using Simple.Services.Account;
using SimpleApp.Common;

namespace Simple.Services.Account;

public class AccountService
{
    private readonly SimpleDbContext _context;

    public AccountService(SimpleDbContext context)
    {
        _context = context;
    }

    public Task<ApiResult<string>> LoginAsync(LoginInput input)
    {
        _context.Set<SysUserRole>().Include(t => t.Role);

        var jwtTokenModel = new JwtTokenModel(input.Account, "admin");
        var token = JwtHelper.Create(jwtTokenModel);

        return Task.FromResult(ApiResult<string>.Status200OK(data: token));
    }
}
