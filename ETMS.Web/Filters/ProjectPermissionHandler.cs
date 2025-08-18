using System.Security.Claims;
using ETMS.Service.Services.Interfaces;
using ETMS.Web.Security;
using Microsoft.AspNetCore.Authorization;

namespace ETMS.Web.Filters;

public class ProjectPermissionHandler(IPermissionService permissionService, IHttpContextAccessor httpContext) : AuthorizationHandler<PermissionRequirement>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
        var user = context.User;
        var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId != null)
        {
            bool hasPermission = await permissionService.UserHasPermissionAsync(
                userId:int.Parse(userId),
                requirement.Permission
            );

            if (hasPermission)
            {
                context.Succeed(requirement);
            }
        }
    }
}
