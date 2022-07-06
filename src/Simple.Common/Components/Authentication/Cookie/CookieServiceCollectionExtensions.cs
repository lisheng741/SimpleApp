using Microsoft.AspNetCore.Authentication.Cookies;

namespace Microsoft.Extensions.DependencyInjection;

public static class CookieServiceCollectionExtensions
{
    public static IServiceCollection AddCookieAuthentication(this IServiceCollection services)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));

        services.AddAuthentication(options =>
        {
            options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        })
        .AddCookie(options =>
        {
            options.LoginPath = "/Account/Login";
            options.LogoutPath = "/Account/Logout";
            options.AccessDeniedPath = "/Account/AccessDenied"; //无权访问拒绝地址

            options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
        });

        return services;
    }
}
