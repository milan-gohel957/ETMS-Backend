using ETMS.Domain.Entities;

namespace ETMS.Service.Services.Interfaces;

public interface IPermissionService
{
    Task<bool> UserHasPermissionAsync(int userId, string permission);
    Task<List<string>> GetUserPermissionsAsync(int userId);
    Task<bool> HasPermissionAsync(int userId, int projectId, string permission);
    Task<IEnumerable<string>?> GetUserProjectPermissionsAsync(int userId, int projectId);
    Task<IEnumerable<Permission>> GetAllPermissionsAsync();
    Task<Permission?> GetPermissionByNameAsync(string permissionName);
}
