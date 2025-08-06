using ETMS.Service.DTOs;

namespace ETMS.Service.Services.Interfaces;

public interface ITaskService
{
    Task DeleteTask(int boardId, int taskId);
    Task<TaskDto> CreateTaskAsync(CreateTaskDto createTaskDto);
    Task<IEnumerable<TaskDto>> GetTasksByBoardIdAsync(int boardId);
    Task ShiftTaskOrderRangeAsync(ShiftTaskOrderRangeDto shiftTaskOrderRangeDto);
    Task UpdateTaskPositionsAsync(UpdateTaskPositionDto updateTaskOrderDto);

}
