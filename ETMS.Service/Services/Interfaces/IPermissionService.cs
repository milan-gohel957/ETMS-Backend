using ETMS.Domain.Entities;

namespace ETMS.Service.Services.Interfaces;

public interface IPermissionService
{
    Task<bool> UserHasPermission(int userId, string permission);
    Task<List<string>> GetUserPermissions(int userId);
    Task<bool> HasPermissionAsync(int userId, int projectId, string permission);
    Task<IEnumerable<string>?> GetUserProjectPermissionsAsync(int userId, int projectId);
    Task<IEnumerable<Permission>> GetAllPermissionsAsync();
    Task<Permission?> GetPermissionByName(string permissionName);
}
