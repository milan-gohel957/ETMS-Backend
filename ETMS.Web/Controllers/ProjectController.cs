using System.Security.Claims;
using ETMS.Domain.Entities;
using ETMS.Service.DTOs;
using ETMS.Service.Services.Interfaces;
using ETMS.Web.Filters;
using Microsoft.AspNetCore.Mvc;
using static ETMS.Domain.Enums.Enums;

namespace ETMS.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectController(IProjectService projectService) : ControllerBase
{
    [HttpGet("users/current")]
    [HandleRequestResponse(TypeResponse = ETypeRequestResponse.ResponseWithData)]
    public async Task<ActionResult<IEnumerable<ProjectDto>>> GetCurrentUserProjects()
    {
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!int.TryParse(userIdString, out var userId))
        {
            return Unauthorized("Invalid user identifier.");
        }

        IEnumerable<ProjectDto> projects = await projectService.GetUserProjectsAsync(userId);
        return Ok(projects);
    }

    [HttpDelete("{id}")]
    [HandleRequestResponse]

    public async Task<IActionResult> DeleteProject(int id)
    {
        await projectService.DeleteProjectAsync(id);
        return NoContent();
    }

    [HttpPut("{id}")]
    [HandleRequestResponse]
    public async Task<IActionResult> UpdateProject(int id, [FromBody] UpdateProjectDto projectDto)
    {
        await projectService.UpdateProjectAsync(id, projectDto); // Pass the id explicitly!
        return NoContent();
    }

    [HttpPost]
    [HandleRequestResponse]
    public async Task<IActionResult> CreateProject(CreateProjectDto createProjectDto)
    {
        createProjectDto.CreatedByUserId = GetCurrentUserId();

        ProjectDto createdProjectDto = await projectService.CreateProjectAsync(createProjectDto);
        return CreatedAtAction(nameof(GetProjectById), new { id = createdProjectDto.Id }, createdProjectDto);
    }

    [HttpGet("{id}")]
    [HandleRequestResponse(TypeResponse = ETypeRequestResponse.ResponseWithData)]
    public async Task<ActionResult<Project>> GetProjectById(int id)
    {
        var result = await projectService.GetProjectByIdAsync(id);
        return Ok(result);
    }
    
    private int GetCurrentUserId()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
        {
            throw new UnauthorizedAccessException("Invalid user identifier");
        }

        return userId;
    }
}
