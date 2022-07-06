using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple.Common.DependencyInjection;

namespace Simple.Services
{
    [AutoInjection(true)]
    public class TestService
    {
        public string Get()
        {
            return "Get test";
        }

        public string GetToken()
        {
            return "";
        }
    }
}
