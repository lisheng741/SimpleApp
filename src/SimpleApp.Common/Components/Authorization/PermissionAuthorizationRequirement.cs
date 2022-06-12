using Microsoft.AspNetCore.Authorization;

namespace SimpleApp.Common.Components.Authorization;

public class PermissionAuthorizationRequirement : IAuthorizationRequirement
{
    public string Name { get; set; }

    public PermissionAuthorizationRequirement(string name)
    {
        Name = name;
    }
}
