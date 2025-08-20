using ETMS.Service.DTOs;

namespace ETMS.Service.Services.Interfaces;

public interface ITaskService
{
    Task DeleteTask(int boardId, int taskId, int userId);
    Task<TaskDto> CreateTaskAsync(CreateTaskDto createTaskDto);
    Task<IEnumerable<TaskDto>> GetTasksByBoardIdAsync(int boardId);
    Task ShiftTaskOrderRangeAsync(ShiftTaskOrderRangeDto shiftTaskOrderRangeDto);
    Task UpdateTaskPositionsAsync(UpdateTaskPositionDto updateTaskOrderDto);
    Task MoveTaskAsync(MoveTaskDto moveTaskDto, int taskId);
    Task UpdateTaskAsync(int boardId, int taskId, UpdateTaskDto updateTaskDto);
    Task AssignTaskToUsers(int taskId, AssignTaskUserDto assignUsersToTaskDto);
    Task<IEnumerable<UserDto>> GetTaskMembersAsync(int taskId);
}
