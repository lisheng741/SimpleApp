using Microsoft.AspNetCore.Authorization;
using Simple.Common.Authorization;

namespace Microsoft.Extensions.DependencyInjection;

public static class AuthorizationServiceCollectionExtensions
{
    public static IServiceCollection AddSimpleAuthorization(this IServiceCollection services)
    {
        services.AddAuthorization();

        services.AddTransient<IAuthorizationPolicyProvider, SimpleAuthorizationPolicyProvider>();
        services.AddTransient<IAuthorizationHandler, SimpleAuthorizationHandler>();
        services.AddTransient<IPermissionChecker, DefaultPermissionChecker>();

        return services;
    }
}
