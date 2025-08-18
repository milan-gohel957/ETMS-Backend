using ETMS.Service.DTOs;
using ETMS.Service.Services;
using ETMS.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ETMS.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize("Admin")]
public class PermissionController(IPermissionService permissionService) : BaseApiController
{
    [HttpPost]
    public async Task<IActionResult> CreateNewPermission([FromBody] string permission)
    {
        PermissionDto permissionDto = await permissionService.CreatePermissionAsync(permission);
        return Success(permissionDto);
    }

    [HttpPost]
    public async Task<IActionResult> UpdatePermission(PermissionDto permission)
    {
        await permissionService.UpdatePermissionAsync(permission.Id, permission.Name);
        return Success("Permission Updated!");
    }

    [HttpPut("/roles/{roleId:int}/permissions/{permissionId:int}")]
    public async Task<IActionResult> TogglePermissionByRoleId(int roleId, int permissionId)
    {
        await permissionService.TogglePermissionByRoleId(permissionId, roleId);
        return Success("Permission Toggled!");
    }

    [HttpPost("/roles/{roldId:int}/permissions/{permision:int}")]
    public async Task<IActionResult> AssignPermissionToRoleId(int roleId, int permissionId)
    {
        await permissionService.AssignPermissionToRoleId(permissionId, roleId);
        return Success("Permission Assigned!");
    }
}
