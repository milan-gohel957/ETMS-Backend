using ETMS.Domain.Entities;
using ETMS.Repository.Repositories.Interfaces;
using ETMS.Service.Services.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;

namespace ETMS.Service.Services;

public class PermissionService(IUnitOfWork unitOfWork, IMemoryCache memoryCache) : IPermissionService
{
    public async Task<Permission?> GetPermissionByName(string permissionName)
    {
        return await unitOfWork.GetRepository<Permission>().FirstOrDefaultAsync(x => x.Name == permissionName);
    }

    public async Task<bool> UserHasPermission(int userId, string permission)
    {
        var rolePermissionRepo = unitOfWork.GetRepository<RolePermission>();

        // Single query with all necessary joins
        var hasPermission = await rolePermissionRepo.AnyAsync(rp =>
            rp.Role.UserRoles.Any(ur => ur.UserId == userId) &&
            rp.Permission.Name == permission);

        return hasPermission;
    }

    public async Task<List<string>> GetUserPermissions(int userId)
    {
        var rolePermissionRepo = unitOfWork.GetRepository<RolePermission>();
        var roleRepo = unitOfWork.GetRepository<Role>();
        var userRoleRepo = unitOfWork.GetRepository<UserRole>();

        IEnumerable<UserRole> userRoles = await userRoleRepo.GetAllAsync(x => x.UserId == userId);

        List<string> userPermissions = new();

        foreach (var user in userRoles)
        {
            var rolePermission = await rolePermissionRepo.GetAllWithIncludesAsync(x => x.RoleId == user.RoleId, y => y.Permission);

            foreach (var role in rolePermission)
            {
                userPermissions.Add(role.Permission.Name);
            }
        }

        return userPermissions;
    }


    public async Task<bool> HasPermissionAsync(int userId, int projectId, string permission)
    {
        string cacheKey = $"policy_permission_{userId}_{projectId}_{permission}";

        if (memoryCache.TryGetValue(cacheKey, out bool cachedResult))
        {
            return cachedResult;
        }

        var roleRepo = unitOfWork.GetRepository<Role>();
        var userProjectRoleRepo = unitOfWork.GetRepository<UserProjectRole>();
        var rolePermissionRepo = unitOfWork.GetRepository<RolePermission>();


        UserProjectRole? userProjectRole = await userProjectRoleRepo.FirstOrDefaultAsync(upr => upr.ProjectId == projectId && upr.UserId == userId);
        if (userProjectRole == null) return false;

        bool hasPermission = await rolePermissionRepo.AnyAsync(rp => rp.RoleId == userProjectRole.RoleId && rp.Permission.Name == permission);

        memoryCache.Set(cacheKey, hasPermission, TimeSpan.FromHours(1));

        return hasPermission;
    }

    public async Task<IEnumerable<string>?> GetUserProjectPermissionsAsync(int userId, int projectId)
    {

        var cacheKey = $"project_permissions_{userId}_{projectId}";

        // Check cache first
        if (memoryCache.TryGetValue(cacheKey, out List<string>? cachedPermissions))
        {
            return cachedPermissions;
        }

        // Fetch user's role in the project
        var userProjectRole = await unitOfWork.GetRepository<UserProjectRole>()
            .FirstOrDefaultAsync(upr =>
                upr.UserId == userId && upr.ProjectId == projectId);

        if (userProjectRole == null)
        {
            return new List<string>();
        }

        // Fetch permissions for the role
        var permissions = (await unitOfWork.GetRepository<RolePermission>()
            .GetAllAsync(rp => rp.RoleId == userProjectRole.RoleId))
            .ToList()
            .Select(rp => rp.Permission.Name);

        // Cache the result
        memoryCache.Set(cacheKey, permissions,
            new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromHours(1)));

        return permissions;

    }

    public async Task<IEnumerable<Permission>> GetAllPermissionsAsync()
    {
        return await unitOfWork.GetRepository<Permission>().GetAllAsync();
    }
}