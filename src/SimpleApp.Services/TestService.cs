using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleApp.Services.Contracts;

namespace SimpleApp.Services;

public class TestService: ITestService
{
    public string Get()
    {
        return "test service";
    }
}
