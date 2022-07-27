using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple.Common.DependencyInjection;
using Simple.Common.Services;

namespace Simple.Services
{
    [AutoInjection(true)]
    public class TestService
    {
        private ICurrentUserService _currentUser;

        public TestService(ICurrentUserService currentUser)
        {
            _currentUser = currentUser;
        }

        public string Get()
        {
            return _currentUser.Username;
        }

        public string GetToken()
        {
            return "";
        }
    }
}
