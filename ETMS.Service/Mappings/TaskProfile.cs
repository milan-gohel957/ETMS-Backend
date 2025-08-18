using ETMS.Domain.Entities;
using ETMS.Service.DTOs;
using AutoMapper;
using ETMS.Service.Mappings.Interfaces;

namespace ETMS.Service.Mappings;

public class TaskProfile : Profile, IAutoMapperProfile
{
    public TaskProfile()
    {

        CreateMap<ProjectTask, TaskDto>();

        CreateMap<CreateTaskDto, ProjectTask>()
          .ForMember(dest => dest.Status, opt => opt.Ignore());

        CreateMap<UpdateTaskDto, ProjectTask>()
           .ForMember(dest => dest.Status, opt => opt.Ignore());
    }
}
