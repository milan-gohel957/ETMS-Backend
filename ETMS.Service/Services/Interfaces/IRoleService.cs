using ETMS.Service.DTOs;

namespace ETMS.Service.Services.Interfaces;

public interface IRoleService
{
    Task<IEnumerable<RoleDto>> GetAllRolesAsync();
}
