using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleApp.Services.Contracts;

public interface ITestService
{
    string Get();

    string GetToken();
}
