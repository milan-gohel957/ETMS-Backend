using System.Security.Claims;
using ETMS.Service.DTOs;
using ETMS.Service.Exceptions;
using ETMS.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using static ETMS.Domain.Enums.Enums;


namespace ETMS.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectController(IProjectService projectService) : BaseApiController
{
    [HttpGet("users/current")]
    public async Task<IActionResult> GetCurrentUserProjects()
    {
        var userId = GetCurrentUserId(); // Use the helper method for consistency
        IEnumerable<ProjectDto> projects = await projectService.GetUserProjectsAsync(userId);
        return Success(projects, "Current user's projects retrieved successfully.");
    }

    [HttpDelete("{projectId:int}")]
    public async Task<IActionResult> DeleteProject(int projectId)
    {
        await projectService.DeleteProjectAsync(projectId, GetCurrentUserId());

        return Success<object>(null, "Project deleted successfully.");
    }

    [HttpPut("{projectId:int}")]
    public async Task<IActionResult> UpdateProject(int projectId, [FromBody] UpdateProjectDto projectDto)
    {
        await projectService.UpdateProjectAsync(projectId, projectDto);

        return Success<object>(null, "Project updated successfully.");
    }

    [HttpPost("{projectId:int}/users")]
    public async Task<IActionResult> AddUsersToProject(int projectId, AddUsersToProjectDto addUsersToProjectDto)
    {
        await projectService.AddUsersToProject(projectId, addUsersToProjectDto);
        return Success("Users Added to Project!");
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateProject(CreateProjectDto createProjectDto)
    {
        createProjectDto.CreatedByUserId = GetCurrentUserId();

        ProjectDto createdProjectDto = await projectService.CreateProjectAsync(createProjectDto);

        return Success<object>(createdProjectDto, "project created successfully.");
    }

    [HttpGet("{projectId:int}")]
    public async Task<IActionResult> GetProjectById(int projectId)
    {
        var project = await projectService.GetProjectByIdAsync(projectId, GetCurrentUserId());

        return Success<object>(project, "project retrived successfully.");
    }

    private int GetCurrentUserId()
    {
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userIdString) || !int.TryParse(userIdString, out var userId))
        {
            // Throwing an exception is better here, as your filter will handle it
            throw new ResponseException(EResponse.Unauthorized, "Invalid user identifier in token.");
        }

        return userId;
    }
}