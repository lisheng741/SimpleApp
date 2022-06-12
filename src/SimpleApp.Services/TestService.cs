using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleApp.Common.Components.Authentication.Jwt;
using SimpleApp.Services.Contracts;

namespace SimpleApp.Services;

public class TestService: ITestService
{
    public string Get()
    {
        return "test service";
    }

    public string GetToken()
    {
        return JwtHelper.IssueJwt(new JwtTokenModel("admin", "role_admin"));
    }
}
