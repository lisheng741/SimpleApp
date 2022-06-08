using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleApp.Common.Authentication.Jwt
{
    public class JwtTokenModel
    {
        public string Username { get; set; }
        public string[] Roles { get; set; }

        public JwtTokenModel(string username, string[] roles)
        {
            Username = username;
            Roles = roles;
        }
    }
}
