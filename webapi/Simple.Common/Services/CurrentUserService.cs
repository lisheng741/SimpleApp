using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Simple.Common.Services;

public interface ICurrentUserService
{
    public ClaimsPrincipal User { get; }
    public string Username { get; }
}

public class CurrentUserService : ICurrentUserService
{
    private readonly ISimpleService _simpleService;

    public CurrentUserService(ISimpleService simpleService)
    {
        _simpleService = simpleService;
    }

    public ClaimsPrincipal User => _simpleService.HttpContext.User;

    public string Username => GetUsername();

    public string GetUsername()
    {
        if (User.Identity == null || User.Identity.IsAuthenticated || string.IsNullOrEmpty(User.Identity.Name))
        {
            return string.Empty;
        }

        return User.Identity.Name;
    }

    public string[] GetRoles()
    {
        return Array.Empty<string>();
    }

    private bool IsAuthenticated()
    {
        if (User.Identity == null || User.Identity.IsAuthenticated)
        {
            return true;
        }
        return false;
    }
}
