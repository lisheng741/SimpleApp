using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Simple.Common.Authentication.Jwt;

namespace Simple.Services;

public class AccountService
{
    private readonly SimpleDbContext _context;

    public AccountService(SimpleDbContext context)
    {
        _context = context;
    }

    public Task<string> CreateTokenAsync(LoginModel login)
    {
        var jwtTokenModel = new JwtTokenModel(login.Account, "admin");
        string token = JwtHelper.Create(jwtTokenModel);

        return Task.FromResult(token);
    }
}
