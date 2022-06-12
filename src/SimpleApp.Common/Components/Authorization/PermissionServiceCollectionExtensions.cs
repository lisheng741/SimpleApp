using Microsoft.AspNetCore.Authorization;
using SimpleApp.Common.Components.Authorization;

namespace Microsoft.Extensions.DependencyInjection;

public static class PermissionServiceCollectionExtensions
{
    public static IServiceCollection AddPermission(this IServiceCollection services)
    {
        services.AddTransient<IAuthorizationHandler, PermissionAuthorizationHandler>();
        services.AddSingleton<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();

        services.AddAuthorization();

        return services;
    }
}
