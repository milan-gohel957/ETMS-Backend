namespace ETMS.Service.Mappings;

using AutoMapper;
using ETMS.Domain.Entities;
using ETMS.Service.DTOs;


public class ProjectProfile : Profile
{
    public ProjectProfile()
    {

        CreateMap<Project, ProjectDto>();

        // If you had a DTO for creating a project, the mapping would be the other way.
        CreateMap<CreateProjectDto, Project>();
        CreateMap<UpdateProjectDto, Project>();
    }
}