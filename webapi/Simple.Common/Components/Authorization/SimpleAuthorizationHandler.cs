using Microsoft.AspNetCore.Authorization;

namespace Simple.Common.Authorization;

public class SimpleAuthorizationHandler : AuthorizationHandler<SimpleAuthorizationRequirement>
{
    private readonly IPermissionChecker _checker;

    public SimpleAuthorizationHandler(IPermissionChecker checker)
    {
        _checker = checker;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, SimpleAuthorizationRequirement requirement)
    {
        if(await _checker.IsGrantedAsync(context.User, requirement.Name))
        {
            context.Succeed(requirement);
        }
    }
}
