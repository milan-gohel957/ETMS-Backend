using ETMS.Domain.Common;
using ETMS.Service.DTOs;
using ETMS.Service.Services.Interfaces;
using ETMS.Web.Filters;
using Microsoft.AspNetCore.Mvc;

namespace ETMS.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskController(ITaskService taskService) : ControllerBase
{
    [HandleRequestResponse(TypeResponse = Domain.Enums.Enums.ETypeRequestResponse.ResponseWithData)]
    [HttpPost]
    public async Task<Response<TaskDto>> CreateProjectTask(CreateTaskDto createTaskDto)
    {
        TaskDto taskDto = await taskService.CreateTaskAsync(createTaskDto);
        return new Response<TaskDto>
        {
            Data = taskDto,
            Errors = [],
            Message = "Task Created Successfully!",
            StatusCode = System.Net.HttpStatusCode.OK,
            Succeeded = true
        };
    }

    [HttpGet("{boardId}")]
    [HandleRequestResponse(TypeResponse = Domain.Enums.Enums.ETypeRequestResponse.ResponseWithData)]
    public async Task<Response<IEnumerable<TaskDto>>> GetTasksByBoardId(int boardId)
    {
        IEnumerable<TaskDto> taskDtos = await taskService.GetTasksByBoardIdAsync(boardId);
        return new Response<IEnumerable<TaskDto>>
        {
            Data = taskDtos,
            Errors = [],
            Message = "Success",
            StatusCode = System.Net.HttpStatusCode.OK,
            Succeeded = true
        };
    }

    [HttpDelete("{boardId}/{taskId}")]
    [HandleRequestResponse]
    public async Task<Response<object>> DeleteTask(int boardId, int taskId)
    {
        await taskService.DeleteTask(boardId, taskId);
        return new Response<object>
        {
            Data = null,
            Errors = [],
            Message = "Task Deleted",
            StatusCode = System.Net.HttpStatusCode.OK,
            Succeeded = true,
        };
    }

    [HttpPost("shift-range")]
    [HandleRequestResponse]
    public async Task<Response<object>> ShiftTaskOrderAsync([FromBody] ShiftTaskOrderRangeDto dto)
    {
        await taskService.ShiftTaskOrderRangeAsync(dto);
        return new Response<object>
        {
            Data = null,
            Errors = [],
            Message = "Task order shifted",
            StatusCode = System.Net.HttpStatusCode.OK,
            Succeeded = true,
        };
    }
    [HttpPatch("update-positions")]
    [HandleRequestResponse]
    public async Task<Response<object>> UpdateTaskPositionsAsync([FromBody] UpdateTaskPositionDto updateTaskPositionDto)
    {
        await taskService.UpdateTaskPositionsAsync(updateTaskPositionDto);
        return new Response<object>
        {
            Data = null,
            Errors = [],
            Message = "Task position updated",
            StatusCode = System.Net.HttpStatusCode.OK,
            Succeeded = true,
        };
    }

    [HttpPost("move")]
    public async Task<Response<object>> MoveTaskAsync([FromBody] MoveTaskDto moveTaskDto)
    {
        await taskService.MoveTaskAsync(moveTaskDto);
        return new Response<object>
        {
            Data = null,
            Errors = [],
            Message = "Task moved",
            StatusCode = System.Net.HttpStatusCode.OK,
            Succeeded = true,
        };
    }
}
