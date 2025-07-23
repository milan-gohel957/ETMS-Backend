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
            var projectIdString = httpContext.HttpContext.Request.RouteValues["projectId"]?.ToString()
                             ?? httpContext.HttpContext.Request.Query["projectId"].ToString();

            if (!int.TryParse(projectIdString, out var projectId))
            {
                context.Fail(new AuthorizationFailureReason(this, "Project ID not found in request."));
                return;
            }
            bool hasPermission = await permissionService.HasPermissionAsync(
                int.Parse(userId),
                projectId,
                requirement.Permission
            );

            if (hasPermission)
            {
                context.Succeed(requirement);
            }
        }
    }
}
