using ETMS.Domain.Common;
using ETMS.Service.DTOs;
using ETMS.Service.Services.Interfaces;
using ETMS.Web.Filters;
using Microsoft.AspNetCore.Mvc;

namespace ETMS.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskController(ITaskService taskService) : BaseApiController
{
    [HttpPost]
    public async Task<IActionResult> CreateProjectTask(CreateTaskDto createTaskDto)
    {
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
        await taskService.DeleteTask(boardId, taskId);
        return Success<object>(null, "Task deleted successfully.");
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
        await taskService.UpdateTaskPositionsAsync(updateTaskPositionDto);
        return Success<object>(null, "Task Position Updated Successfully.");
    }

    [HttpPost("move/{taskId:int}")]
    public async Task<IActionResult> MoveTaskAsync([FromBody] MoveTaskDto moveTaskDto, int taskId)
    {
        await taskService.MoveTaskAsync(moveTaskDto, taskId);
        return Success("Task moved successfully.");
    }

    [HttpPut("{taskId}")]
    public async Task<IActionResult> UpdateTask(int taskId, [FromBody] UpdateTaskDto taskDto)
    {
        await taskService.UpdateTaskAsync(taskDto.BoardId, taskId,taskDto);
        return Success<object>(null, "Task updated successfully.");
    }
}
