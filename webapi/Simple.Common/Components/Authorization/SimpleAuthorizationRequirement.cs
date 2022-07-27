using Microsoft.AspNetCore.Authorization;

namespace Simple.Common.Authorization;

public class SimpleAuthorizationRequirement : IAuthorizationRequirement
{
    public string Name { get; set; }

    public SimpleAuthorizationRequirement(string name)
    {
        Name = name;
    }
}
