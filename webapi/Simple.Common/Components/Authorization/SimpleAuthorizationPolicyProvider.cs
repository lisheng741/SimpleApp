using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace Simple.Common.Authorization;

public class SimpleAuthorizationPolicyProvider : DefaultAuthorizationPolicyProvider, IAuthorizationPolicyProvider
{
    public SimpleAuthorizationPolicyProvider(IOptions<AuthorizationOptions> options) 
        : base(options)
    { }

    public new Task<AuthorizationPolicy> GetDefaultPolicyAsync()
    {
        var policyBuilder = new AuthorizationPolicyBuilder(Array.Empty<string>());
        policyBuilder.AddRequirements(new SimpleAuthorizationRequirement(""));
        var policy = policyBuilder.Build();
        return Task.FromResult(policy);
    }

    public override async Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
    {
        var policy = await base.GetPolicyAsync(policyName);
        if(policy != null)
        {
            return policy;
        }

        if(!string.IsNullOrEmpty(policyName))
        {
            var policyBuilder = new AuthorizationPolicyBuilder(Array.Empty<string>());
            policyBuilder.AddRequirements(new SimpleAuthorizationRequirement(policyName));
            return policyBuilder.Build();
        }

        return null;
    }
}
