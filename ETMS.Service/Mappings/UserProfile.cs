using AutoMapper;
using ETMS.Domain.Common;
using ETMS.Domain.Entities;
using ETMS.Service.DTOs;
using ETMS.Service.Mappings.Interfaces;

namespace ETMS.Service.Mappings;

public class UserProfile:Profile, IAutoMapperProfile
{
    public UserProfile()
    {
        CreateMap<User, CurrentUserDto>();
        CreateMap<User, UserDto>();
    }
}