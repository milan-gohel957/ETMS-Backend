using AutoMapper;
using ETMS.Domain.Entities;
using ETMS.Repository.Repositories.Interfaces;
using ETMS.Service.DTOs;
using ETMS.Service.Exceptions;
using ETMS.Service.Services.Interfaces;
using static ETMS.Domain.Enums.Enums;

namespace ETMS.Service.Services;

public class TaskService(IUnitOfWork unitOfWork, IMapper mapper) : ITaskService
{
    IGenericRepository<ProjectTask> _taskRepository = unitOfWork.GetRepository<ProjectTask>();
    IGenericRepository<Board> _boardRepository = unitOfWork.GetRepository<Board>();
    public async Task<IEnumerable<TaskDto>> GetTasksByBoardIdAsync(int boardId)
    {
        bool isBoardExists = await _boardRepository.ExistsAsync(boardId);
        if (!isBoardExists)
            throw new ResponseException(EResponse.NotFound, "Board Not Found.");

        return mapper.Map<IEnumerable<TaskDto>>(await _taskRepository.GetAllAsync(t => t.BoardId == boardId));
    }

    public async Task DeleteTask(int boardId, int taskId)
    {
        bool isBoardExists = await _boardRepository.ExistsAsync(boardId);
        if (!isBoardExists)
            throw new ResponseException(EResponse.NotFound, "Board Not Found.");

        bool isTaskExists = await _taskRepository.AnyAsync(t => t.Id == taskId && t.BoardId == boardId);

        if (!isTaskExists)
            throw new ResponseException(EResponse.NotFound, "Task Not Found");

        await _taskRepository.SoftDeleteByIdAsync(taskId);
    }

    public async Task<TaskDto> CreateTaskAsync(CreateTaskDto createTaskDto)
    {
        ProjectTask addedTask = await _taskRepository.AddAsync(mapper.Map<ProjectTask>(createTaskDto));
        await unitOfWork.SaveChangesAsync();
        return mapper.Map<TaskDto>(addedTask);
    }

    public async Task UpdateTaskPositionsAsync(UpdateTaskPositionDto updateTaskOrderDto)
    {
        bool isBoardExists = await _boardRepository.ExistsAsync(updateTaskOrderDto.NewBoardId);
        if (!isBoardExists)
            throw new ResponseException(EResponse.NotFound, "Board Not Found.");
        //Update the Board Id and Task Order
        ProjectTask? dbTask = await _taskRepository.GetByIdAsync(updateTaskOrderDto.TaskId);
        if (dbTask == null)
            throw new ResponseException(EResponse.NotFound, "Task Not Found.");

        dbTask.BoardId = updateTaskOrderDto.NewBoardId;

        dbTask.Order = updateTaskOrderDto.NewPosition;

        _taskRepository.Update(dbTask);

        await unitOfWork.SaveChangesAsync();
    }

    public async Task ShiftTaskOrderRangeAsync(ShiftTaskOrderRangeDto shiftTaskOrderRangeDto)
    {
        bool isBoardExists = await _boardRepository.ExistsAsync(shiftTaskOrderRangeDto.BoardId);
        if (!isBoardExists)
            throw new ResponseException(EResponse.NotFound, "Board Not Found.");

        if (shiftTaskOrderRangeDto.ShiftAmount == 0)
            throw new ResponseException(EResponse.BadRequest, "Shift Amount cannot be zero.");


        var tasksToShift = await _taskRepository.GetAllAsync(t => t.BoardId == shiftTaskOrderRangeDto.BoardId
         && t.Order >= shiftTaskOrderRangeDto.MinOrder && t.Order <= shiftTaskOrderRangeDto.MaxOrder);


        if (!tasksToShift.Any())
            return;

        foreach (var task in tasksToShift)
        {
            task.Order += shiftTaskOrderRangeDto.ShiftAmount;
        }

        _taskRepository.UpdateRange(tasksToShift);
        
        await unitOfWork.SaveChangesAsync();
    }
}
