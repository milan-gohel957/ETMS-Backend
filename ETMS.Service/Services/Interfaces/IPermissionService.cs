using ETMS.Domain.Entities;
using ETMS.Service.DTOs;

namespace ETMS.Service.Services.Interfaces;

public interface IPermissionService
{
    Task<Permission?> GetPermissionByNameAsync(string permissionName);
    Task<bool> UserHasPermissionAsync(int userId, string permission);
    Task<List<string>> GetUserPermissionsAsync(int userId);
    Task<bool> HasPermissionAsync(int userId, int projectId, string permission);
    Task<IEnumerable<string>> GetUserProjectPermissionsAsync(int userId, int projectId);
    Task<IEnumerable<Permission>> GetAllPermissionsAsync();
    Task<PermissionDto> CreatePermissionAsync(string newPermission);
    Task<PermissionDto?> UpdatePermissionAsync(int permissionId, string newName);
    Task<IEnumerable<PermissionDto>> UpdatePermissionsAsync(IEnumerable<PermissionDto> permissionDtos);
    Task AssignPermissionToRoleId(int permissionId, int roleId);
    Task TogglePermissionByRoleId(int permissionId, int roleId);
}
