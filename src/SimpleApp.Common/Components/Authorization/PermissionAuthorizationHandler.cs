using Microsoft.AspNetCore.Authorization;

namespace SimpleApp.Common.Components.Authorization;

public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionAuthorizationRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionAuthorizationRequirement requirement)
    {
        

        return Task.CompletedTask;
    }
}
