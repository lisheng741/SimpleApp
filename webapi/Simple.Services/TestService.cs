using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Simple.Common.DependencyInjection;
using Simple.Common.Services;

namespace Simple.Services;


public interface ITestService
{
    string Get();

    string GetToken();
}

[AutoInjection(true)]
public class TestService : ITestService
{
    private readonly ISimpleService _simpleService;
    private ICurrentUserService _currentUser;

    public TestService(ICurrentUserService currentUser, ISimpleService simpleService)
    {
        _currentUser = currentUser;
        _simpleService = simpleService;
    }

    public string Get()
    {

        return _currentUser.UserName;
    }

    public string GetToken()
    {
        return "";
    }
}
