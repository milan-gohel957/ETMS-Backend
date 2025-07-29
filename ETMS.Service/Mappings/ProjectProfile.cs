namespace ETMS.Service.Mappings;

using AutoMapper;
using ETMS.Domain.Entities;
using ETMS.Service.DTOs;
using ETMS.Service.Mappings.Interfaces;

public class ProjectProfile : Profile, IAutoMapperProfile
{
    public ProjectProfile()
    {

        CreateMap<Project, ProjectDto>();

        // If you had a DTO for creating a project, the mapping would be the other way.
        CreateMap<CreateProjectDto, Project>();
        CreateMap<UpdateProjectDto, Project>();
    }
}