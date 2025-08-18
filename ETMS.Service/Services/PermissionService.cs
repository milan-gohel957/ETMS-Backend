using ETMS.Domain.Entities;
using ETMS.Repository.Repositories.Interfaces;
using ETMS.Service.DTOs;
using ETMS.Service.Exceptions;
using ETMS.Service.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using static ETMS.Domain.Enums.Enums;

namespace ETMS.Service.Services;

public class PermissionService(IUnitOfWork unitOfWork, IMemoryCache memoryCache) : IPermissionService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMemoryCache _memoryCache = memoryCache;
    private readonly IGenericRepository<Permission> _permissionRepository = unitOfWork.GetRepository<Permission>();
    private readonly IGenericRepository<RolePermission> _rolePermissionRepository = unitOfWork.GetRepository<RolePermission>();
    private readonly IGenericRepository<Role> _roleRepository = unitOfWork.GetRepository<Role>();

    public async Task<Permission?> GetPermissionByNameAsync(string permissionName)
    {
        return await _permissionRepository.FirstOrDefaultAsync(x => x.Name == permissionName);
    }

    public async Task<bool> UserHasPermissionAsync(int userId, string permission)
    {
        var rolePermissionRepo = _unitOfWork.GetRepository<RolePermission>();

        return await rolePermissionRepo.AnyAsync(rp =>
            rp.Role!.UserRoles.Any(ur => ur.UserId == userId) &&
            rp.Permission!.Name == permission);
    }

    public async Task<List<string>> GetUserPermissionsAsync(int userId)
    {
        var rolePermissionRepo = _unitOfWork.GetRepository<RolePermission>();
        var userRoleRepo = _unitOfWork.GetRepository<UserRole>();

        IEnumerable<UserRole> userRoles = await userRoleRepo.GetAllAsync(x => x.UserId == userId);

        List<string> userPermissions = new();

        foreach (var user in userRoles)
        {
            var rolePermission = await rolePermissionRepo.GetAllWithIncludesAsync(
                x => x.RoleId == user.RoleId,
                includes: y => y.Permission!);

            foreach (var role in rolePermission)
            {
                userPermissions.Add(role.Permission!.Name);
            }
        }

        return userPermissions;
    }

    public async Task<bool> HasPermissionAsync(int userId, int projectId, string permission)
    {
        string cacheKey = $"policy_permission_{userId}_{projectId}_{permission}";

        if (_memoryCache.TryGetValue(cacheKey, out bool cachedResult))
        {
            return cachedResult;
        }

        var userProjectRoleRepo = _unitOfWork.GetRepository<UserProjectRole>();
        var rolePermissionRepo = _unitOfWork.GetRepository<RolePermission>();

        UserProjectRole? userProjectRole = await userProjectRoleRepo.FirstOrDefaultAsync(
            upr => upr.ProjectId == projectId && upr.UserId == userId);

        if (userProjectRole == null) return false;

        bool hasPermission = await rolePermissionRepo.AnyAsync(rp =>
            rp.RoleId == userProjectRole.RoleId && rp.Permission!.Name == permission);

        _memoryCache.Set(cacheKey, hasPermission, TimeSpan.FromHours(1));

        return hasPermission;
    }

    public async Task<IEnumerable<string>> GetUserProjectPermissionsAsync(int userId, int projectId)
    {
        var cacheKey = $"project_permissions_{userId}_{projectId}";

        if (_memoryCache.TryGetValue(cacheKey, out List<string>? cachedPermissions))
        {
            return cachedPermissions;
        }

        var userProjectRoleRepo = _unitOfWork.GetRepository<UserProjectRole>();

        var userProjectRole = await userProjectRoleRepo.FirstOrDefaultAsync(
            upr => upr.UserId == userId && upr.ProjectId == projectId);

        if (userProjectRole == null)
        {
            return new List<string>();
        }

        var rolePermissionRepo = _unitOfWork.GetRepository<RolePermission>();

        var permissions = (await rolePermissionRepo.GetAllAsync(rp => rp.RoleId == userProjectRole.RoleId))
            .Select(rp => rp.Permission!.Name)
            .ToList();

        _memoryCache.Set(cacheKey, permissions,
            new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromHours(1)));

        return permissions;
    }

    public async Task<IEnumerable<Permission>> GetAllPermissionsAsync()
    {
        return await _permissionRepository.GetAllAsync();
    }

    public async Task<PermissionDto> CreatePermissionAsync(string newPermission)
    {
        Permission permission = new()
        {
            Name = newPermission,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            IsDeleted = false
        };

        await _permissionRepository.AddAsync(permission);
        await _unitOfWork.SaveChangesAsync();

        return new PermissionDto
        {
            Id = permission.Id,
            Name = permission.Name
        };
    }

    public async Task<PermissionDto?> UpdatePermissionAsync(int permissionId, string newName)
    {
        var permission = await _permissionRepository.GetByIdAsync(permissionId);
        if (permission == null) return null;

        permission.Name = newName;
        permission.UpdatedAt = DateTime.UtcNow;

        _permissionRepository.Update(permission);
        await _unitOfWork.SaveChangesAsync();

        return new PermissionDto
        {
            Id = permission.Id,
            Name = permission.Name
        };
    }

    public async Task TogglePermissionByRoleId(int permissionId, int roleId)
    {
        RolePermission? rolePermission = await _rolePermissionRepository.FirstOrDefaultAsync(rp => rp.PermissionId == permissionId && rp.RoleId == roleId);

        if (rolePermission == null)
            throw new ResponseException(EResponse.NotFound, $"Permission with {permissionId} or role with {roleId} not found.");

        rolePermission.IsActive = !rolePermission.IsActive;

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task AssignPermissionToRoleId(int permissionId, int roleId)
    {
        bool isPermissionExists = await _permissionRepository.ExistsAsync(roleId);
        bool isRoleExists = await _roleRepository.ExistsAsync(roleId);

        if (!isPermissionExists)
            throw new ResponseException(EResponse.NotFound, $"Permission with {permissionId} not found.");

        if (!isRoleExists)
            throw new ResponseException(EResponse.NotFound, $"Role with {roleId} Not found.");


        RolePermission rolePermission = new()
        {
            RoleId = roleId,
            PermissionId = permissionId
        };

        await _rolePermissionRepository.AddAsync(rolePermission);
    }

    public async Task<IEnumerable<PermissionDto>> UpdatePermissionsAsync(IEnumerable<PermissionDto> permissionDtos)
    {
        List<PermissionDto> updatedPermissions = new();

        foreach (var dto in permissionDtos)
        {
            var permission = await _permissionRepository.GetByIdAsync(dto.Id);
            if (permission == null) continue;

            permission.Name = dto.Name;
            permission.UpdatedAt = DateTime.UtcNow;

            _permissionRepository.Update(permission);
            updatedPermissions.Add(new PermissionDto
            {
                Id = permission.Id,
                Name = permission.Name
            });
        }

        await _unitOfWork.SaveChangesAsync();

        return updatedPermissions;
    }
}
