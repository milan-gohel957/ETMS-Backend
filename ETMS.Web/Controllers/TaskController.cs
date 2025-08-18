using System.Security.Claims;
using ETMS.Domain.Common;
using ETMS.Service.DTOs;
using ETMS.Service.Exceptions;
using ETMS.Service.Services.Interfaces;
using ETMS.Web.Filters;
using Microsoft.AspNetCore.Mvc;
using static ETMS.Domain.Enums.Enums;

namespace ETMS.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskController(ITaskService taskService) : BaseApiController
{
    [HttpPost]
    public async Task<IActionResult> CreateProjectTask(CreateTaskDto createTaskDto)
    {
        createTaskDto.CreatedByUserId = GetCurrentUserId();
        TaskDto taskDto = await taskService.CreateTaskAsync(createTaskDto);
        return Success(taskDto);
    }

    [HttpGet("{boardId}")]
    public async Task<IActionResult> GetTasksByBoardId(int boardId)
    {
        IEnumerable<TaskDto> taskDtos = await taskService.GetTasksByBoardIdAsync(boardId);
        return Success(taskDtos);
    }

    [HttpDelete("{boardId}/{taskId}")]
    public async Task<IActionResult> DeleteTask(int boardId, int taskId)
    {
        int userId = GetCurrentUserId();
        await taskService.DeleteTask(boardId, taskId, userId);
        return Success("Task deleted successfully.");
    }

    [HttpPost("{taskId:int}/assignees")]
    public async Task<IActionResult> AssignTaskToUsers(int taskId, AssignTaskUserDto assignTaskUserDto)
    {
        await taskService.AssignTaskToUsers(taskId, assignTaskUserDto);
        return Success("Task Assigned Successfully!");
    }

    [HttpPost("shift-range")]
    public async Task<IActionResult> ShiftTaskOrderAsync([FromBody] ShiftTaskOrderRangeDto dto)
    {
        await taskService.ShiftTaskOrderRangeAsync(dto);
        return Success<object>(null, "Task order shifted");
    }
    [HttpPatch("update-positions")]
    public async Task<IActionResult> UpdateTaskPositionsAsync([FromBody] UpdateTaskPositionDto updateTaskPositionDto)
    {
        updateTaskPositionDto.UpdatedByUserId = GetCurrentUserId();
        await taskService.UpdateTaskPositionsAsync(updateTaskPositionDto);
        return Success<object>(null, "Task Position Updated Successfully.");
    }

    [HttpPost("move/{taskId:int}")]
    public async Task<IActionResult> MoveTaskAsync([FromBody] MoveTaskDto moveTaskDto, int taskId)
    {
        moveTaskDto.UpdatedByUserId = GetCurrentUserId();

        await taskService.MoveTaskAsync(moveTaskDto, taskId);
        return Success("Task moved successfully.");
    }

    [HttpPut("{taskId}")]
    public async Task<IActionResult> UpdateTask(int taskId, [FromBody] UpdateTaskDto taskDto)
    {
        taskDto.UpdatedByUserId = GetCurrentUserId();
        
        await taskService.UpdateTaskAsync(taskDto.BoardId, taskId, taskDto);
        return Success<object>(null, "Task updated successfully.");
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
