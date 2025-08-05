using System.Net;
using System.Security.Claims;
using ETMS.Service.DTOs;
using ETMS.Service.Exceptions;

using ETMS.Service.Services.Interfaces;
using ETMS.Web.Filters;
using Microsoft.AspNetCore.Mvc;
using static ETMS.Domain.Enums.Enums;
using ETMS.Domain.Common;


namespace ETMS.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectController(IProjectService projectService) : ControllerBase
{
    [HttpGet("users/current")]
    [HandleRequestResponse(TypeResponse = ETypeRequestResponse.ResponseWithData)]
    public async Task<Response<IEnumerable<ProjectDto>>> GetCurrentUserProjects()
    {
        var userId = GetCurrentUserId(); // Use the helper method for consistency
        IEnumerable<ProjectDto> projects = await projectService.GetUserProjectsAsync(userId);
        
        return new()
        {
            Data = projects,
            Message = "Current user's projects retrieved successfully.",
            Succeeded = true,
            StatusCode = HttpStatusCode.OK
        };
    }

    [HttpDelete("{id}")]
    [HandleRequestResponse]
    public async Task<Response<object>> DeleteProject(int id)
    {
        await projectService.DeleteProjectAsync(id);
        return new()
        {
            Data = null,
            Message = "Project deleted successfully.",
            Succeeded = true,
            StatusCode = HttpStatusCode.OK
        };
    }

    [HttpPut("{id}")]
    [HandleRequestResponse]
    public async Task<Response<object>> UpdateProject(int id, [FromBody] UpdateProjectDto projectDto)
    {
        await projectService.UpdateProjectAsync(id, projectDto);
        return new()
        {
            Data = null,
            Message = "Project updated successfully.",
            Succeeded = true,
            StatusCode = HttpStatusCode.OK
        };
    }

    [HttpPost]
    [HandleRequestResponse(TypeResponse = ETypeRequestResponse.ResponseWithData)]
    public async Task<Response<ProjectDto>> CreateProject(CreateProjectDto createProjectDto)
    {
        createProjectDto.CreatedByUserId = GetCurrentUserId();

        ProjectDto createdProjectDto = await projectService.CreateProjectAsync(createProjectDto);
        return new()
        {
            Data = createdProjectDto,
            Message = "Project created successfully.",
            Succeeded = true,
            StatusCode = HttpStatusCode.Created
        };
    }

    [HttpGet("{id}")]
    [HandleRequestResponse(TypeResponse = ETypeRequestResponse.ResponseWithData)]
    public async Task<Response<ProjectDto>> GetProjectById(int id)
    {
        var project = await projectService.GetProjectByIdAsync(id);
        return new()
        {
            Data = project,
            Message = "Project retrieved successfully.",
            Succeeded = true,
            StatusCode = HttpStatusCode.OK
        };
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