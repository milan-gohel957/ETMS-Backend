using ETMS.Service.DTOs;
using ETMS.Domain.Entities;

namespace ETMS.Service.Services.Interfaces;

public interface IProjectService
{
    Task<IEnumerable<ProjectDto>> GetUserProjectsAsync(int userId);

    Task<ProjectDto?> GetProjectById(int projectId);
    Task DeleteProjectAsync(int projectId);
    Task CreateProject(CreateProjectDto projectDto);

    Task UpdateProjectAsync(int id,UpdateProjectDto projectDto);
}
