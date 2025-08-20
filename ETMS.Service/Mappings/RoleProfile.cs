using AutoMapper;
using ETMS.Domain.Entities;
using ETMS.Service.DTOs;
using ETMS.Service.Mappings.Interfaces;

namespace ETMS.Service.Mappings;

public class RoleProfile : Profile, IAutoMapperProfile
{
    public RoleProfile()
    {
        CreateMap<Role, RoleDto>();

    
    }
}
