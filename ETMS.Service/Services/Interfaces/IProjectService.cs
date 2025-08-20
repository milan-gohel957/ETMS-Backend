using ETMS.Service.DTOs;
using ETMS.Domain.Entities;

namespace ETMS.Service.Services.Interfaces;

public interface IProjectService
{
    Task<IEnumerable<ProjectDto>> GetUserProjectsAsync(int userId);

    Task<ProjectDto?> GetProjectByIdAsync(int projectId, int userId);
    Task DeleteProjectAsync(int projectId, int userId);
    Task<ProjectDto> CreateProjectAsync(CreateProjectDto projectDto);
    Task AddUsersToProject(int projectId, AddUsersToProjectDto addUsersToProjectDto);

    Task UpdateProjectAsync(int id, UpdateProjectDto projectDto);
}
