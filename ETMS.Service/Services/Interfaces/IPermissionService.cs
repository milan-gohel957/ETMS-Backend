using ETMS.Domain.Entities;
using ETMS.Service.DTOs;

namespace ETMS.Service.Services.Interfaces;

public interface IPermissionService
{
    Task<IEnumerable<PermissionDto>> GetPermissionsByRoleIdAsync(int roleId);

    Task AssignPermissionToRoleId(int permissionId, int roleId);
    Task<PermissionDto> CreatePermissionAsync(string permissionName);
    Task<IEnumerable<Permission>> GetAllPermissionsAsync();
    Task<List<string?>> GetUserPermissionsAsync(int userId);
    Task<IEnumerable<string?>> GetUserProjectPermissionsAsync(int userId, int projectId);
    Task<bool> HasPermissionAsync(int userId, int projectId, string permission);
    Task RevokePermissionFromRoleId(int permissionId, int roleId);
    Task UpdatePermissionAsync(int permissionId, string newName);
    Task<bool> UserHasPermissionAsync(int userId, string permission);
    Task<Permission?> GetPermissionByNameAsync(string permissionName);
}