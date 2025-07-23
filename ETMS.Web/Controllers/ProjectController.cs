using System.Security.Claims;
using ETMS.Service.DTOs;
using ETMS.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ETMS.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectController(IProjectService projectService) : ControllerBase
{
    [HttpGet("my-projects")]
    public async Task<IActionResult> GetUserProjects()
    {
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!int.TryParse(userIdString, out var userId))
        {
            return Unauthorized("Invalid user identifier.");
        }

        IEnumerable<ProjectDto> projects = await projectService.GetUserProjectsAsync(userId);

        return Ok(projects);
    }

    [HttpDelete("")]
    public async Task<IActionResult> DeleteProject([FromQuery] int projectId)
    {
        await projectService.GetProjectById(projectId);
        return Ok("Project Deleted Successfully!");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProject(int id, [FromBody] UpdateProjectDto projectDto)
    {
        await projectService.UpdateProjectAsync(id, projectDto); // Pass the id explicitly!
        return Ok("Project Updated Successfully!");
    }

    [HttpPost]
    public async Task<IActionResult> CreateProject(CreateProjectDto createProjectDto)
    {
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!int.TryParse(userIdString, out var userId))
        {
            return Unauthorized("Invalid user identifier.");
        }
        createProjectDto.CreatedByUserId = userId;
        await projectService.CreateProject(createProjectDto);
        return Ok("Project Created Successfully!");
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProjectById(int id)
    {
        var result = await projectService.GetProjectById(id);
        return Ok(result);
    }
}
