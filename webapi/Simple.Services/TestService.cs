using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    private ICurrentUserService _currentUser;

    public TestService(ICurrentUserService currentUser)
    {
        _currentUser = currentUser;
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
