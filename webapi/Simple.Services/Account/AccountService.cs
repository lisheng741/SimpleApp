using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple.Common.Components.Authentication.Jwt;
using Simple.Services.Account;
using SimpleApp.Common;

namespace Simple.Services.Account;

public class AccountService
{
    private readonly SimpleDbContext _db;

    public AccountService(SimpleDbContext db)
    {
        _db = db;
    }

    public Task<ApiResult<string>> LoginAsync(LoginInput input)
    {
        _db.SysRole.ToList();

        var jwtTokenModel = new JwtTokenModel(input.Account, "admin");
        var token = JwtHelper.Create(jwtTokenModel);

        return Task.FromResult(ApiResult<string>.Status200OK(data: token));
    }
}
