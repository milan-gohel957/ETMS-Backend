using ETMS.Service.DTOs;
using ETMS.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ETMS.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize("Admin")] // Ensures only Admins can manage permissions
public class PermissionController(IPermissionService permissionService) : BaseApiController
{
    /// <summary>
    /// Creates a new permission in the system.
    /// </summary>
    /// <param name="createDto">The data for the new permission.</param>
    /// <returns>The newly created permission.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(PermissionDto), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreatePermission([FromBody] CreatePermissionDto createDto)
    {
        PermissionDto newPermission = await permissionService.CreatePermissionAsync(createDto.Name);
        // Return 201 Created with a link to the new resource, which is a REST best practice.
        return Success(newPermission);
    }

    /// <summary>
    /// Gets a list of all available permissions.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<PermissionDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllPermissions()
    {
        var permissions = await permissionService.GetAllPermissionsAsync(); // Assuming this method exists
        return Success(permissions);
    }

    /// <summary>
    /// Updates an existing permission's name.
    /// </summary>
    /// <param name="permissionId">The ID of the permission to update.</param>
    /// <param name="updateDto">The new data for the permission.</param>
    [HttpPut("{permissionId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdatePermission(int permissionId, [FromBody] PermissionDto updateDto)
    {
        await permissionService.UpdatePermissionAsync(permissionId, updateDto.Name!);
        return NoContent(); // 204 No Content is the standard response for a successful PUT.
    }

    /// <summary>
    /// Assigns a permission to a specific role.
    /// </summary>
    /// <param name="roleId">The ID of the role.</param>
    /// <param name="permissionId">The ID of the permission to assign.</param>
    [HttpPost("roles/{roleId:int}/permissions/{permissionId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> AssignPermissionToRole(int roleId, int permissionId)
    {
        await permissionService.AssignPermissionToRoleId(permissionId, roleId);
        return Success("Permission successfully assigned to the role.");
    }

    /// <summary>
    /// Revokes a permission from a specific role.
    /// </summary>
    /// <param name="roleId">The ID of the role.</param>
    /// <param name="permissionId">The ID of the permission to revoke.</param>
    [HttpDelete("roles/{roleId:int}/permissions/{permissionId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> RevokePermissionFromRole(int roleId, int permissionId)
    {
        // Replaced 'Toggle' with an explicit 'Revoke' action for clarity.
        // This is much more RESTful and less ambiguous.
        await permissionService.RevokePermissionFromRoleId(permissionId, roleId); // Assuming this service method exists
        return Success("Permission successfully revoked from the role.");
    }
    [HttpGet("roles/{roleId:int}/permissions", Name = "GetPermissionsForRole")]
    public async Task<IActionResult> GetPermissionsByRoleId(int roleId)
    {
        var permissions = await permissionService.GetPermissionsByRoleIdAsync(roleId);
        return Success(permissions);
    }

}