using System.Security.Claims;
using ETMS.Service.DTOs;
using ETMS.Service.Exceptions;

using ETMS.Service.Services.Interfaces;
using ETMS.Web.Filters;
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

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProject(int id)
    {
        await projectService.DeleteProjectAsync(id);

        return Success<object>(null, "Project deleted successfully.");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProject(int id, [FromBody] UpdateProjectDto projectDto)
    {
        await projectService.UpdateProjectAsync(id, projectDto);

        return Success<object>(null, "Project updated successfully.");
    }

    [HttpPost]
    public async Task<IActionResult> CreateProject(CreateProjectDto createProjectDto)
    {
        createProjectDto.CreatedByUserId = GetCurrentUserId();

        ProjectDto createdProjectDto = await projectService.CreateProjectAsync(createProjectDto);

        return Success<object>(createdProjectDto, "project created successfully.");
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProjectById(int id)
    {
        var project = await projectService.GetProjectByIdAsync(id);

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