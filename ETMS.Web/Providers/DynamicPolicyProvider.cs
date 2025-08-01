using System.Collections.Concurrent;
using ETMS.Domain.Entities;
using ETMS.Service.Services.Interfaces;
using ETMS.Web.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace ETMS.Web.Providers;

public class DynamicPolicyProvider(IServiceProvider serviceProvider, IOptions<AuthorizationOptions> options) : IAuthorizationPolicyProvider
{
    private readonly DefaultAuthorizationPolicyProvider fallbackPolicyProvider = new DefaultAuthorizationPolicyProvider(options);
    private readonly IDictionary<string, AuthorizationPolicy> _policies = new ConcurrentDictionary<string, AuthorizationPolicy>();

    public Task<AuthorizationPolicy> GetDefaultPolicyAsync()
    {
        return fallbackPolicyProvider.GetDefaultPolicyAsync();
    }

    public Task<AuthorizationPolicy?> GetFallbackPolicyAsync()
    {
        return fallbackPolicyProvider.GetFallbackPolicyAsync();
    }


    public async Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
    {
        // Check if policy exists in cache
        if (_policies.TryGetValue(policyName, out var policy))
        {
            return policy;
        }
        using var scope = serviceProvider.CreateScope();
        IPermissionService? permissionService = scope.ServiceProvider.GetService<IPermissionService>();
        Permission? dbPermission = permissionService != null ? await permissionService.GetPermissionByNameAsync(policyName) : null;
        //canAddUser
        if (dbPermission != null)
        {
            // Create dynamic policy
            var dynamicPolicy = new AuthorizationPolicyBuilder()
                .AddRequirements(new PermissionRequirement(
                    dbPermission.Name
                ))
                .Build();

            // Cache the policy
            _policies[policyName] = dynamicPolicy;
            return dynamicPolicy;
        }

        // Return default policy if no match
        return await fallbackPolicyProvider.GetPolicyAsync(policyName);
    }


}