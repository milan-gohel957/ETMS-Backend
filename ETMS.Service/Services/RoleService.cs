using AutoMapper;
using ETMS.Domain.Entities;
using ETMS.Repository.Repositories.Interfaces;
using ETMS.Service.DTOs;
using ETMS.Service.Services.Interfaces;

namespace ETMS.Service.Services;



public class RoleService(IUnitOfWork unitOfWork, IMapper mapper) : IRoleService
{
    private readonly IGenericRepository<Role> _roleRepository = unitOfWork.GetRepository<Role>();

    public async Task<IEnumerable<RoleDto>> GetAllRolesAsync()
    {
        return mapper.Map<IEnumerable<RoleDto>>(await _roleRepository.GetAllAsync());
    }
}
