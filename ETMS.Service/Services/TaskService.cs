using AutoMapper;
using ETMS.Domain.Entities;
using ETMS.Repository.Repositories.Interfaces;
using ETMS.Service.DTOs;
using ETMS.Service.Exceptions;
using ETMS.Service.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
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
        //Using queryable to optimize the order calculation based on the provided order and addedAtEndOfBoard flag.
        IQueryable<ProjectTask> task = _taskRepository.Table;
        if (createTaskDto.IsAddedAtEndOfBoard)
        {
            var maxOrder = await task.Where(t => t.BoardId == createTaskDto.BoardId).Select(t => (int?)t.Order).MaxAsync();
            createTaskDto.Order = maxOrder == null ? 1000 : maxOrder.Value + 1000;
        }
        else
        {
            var minOrder = await task
            .Where(t => t.BoardId == createTaskDto.BoardId)
            .Select(t => (int?)t.Order)
            .MinAsync();

            createTaskDto.Order = minOrder == null ? 1000 : minOrder.Value - 1000;
        }
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
        var tasksToShift = await _taskRepository.GetAllAsync(t => t.BoardId == shiftTaskOrderRangeDto.BoardId
         && t.Order >= shiftTaskOrderRangeDto.MinOrder);

        if (!tasksToShift.Any())
            return;

        foreach (var task in tasksToShift)
        {
            task.Order += shiftTaskOrderRangeDto.ShiftAmount;
        }

        _taskRepository.UpdateRange(tasksToShift);
    }

    private const int DefaultGap = 1000;
    private const int ShiftGap = 100;
    public async Task MoveTaskAsync(MoveTaskDto moveTaskDto)
    {
        // Start a database transaction to ensure atomicity.
        // If anything fails, the entire operation is rolled back.
        await unitOfWork.BeginTransactionAsync();

        try
        {
            // === 1. VALIDATION ===
            bool isBoardExists = await _boardRepository.ExistsAsync(moveTaskDto.NewBoardId);
            if (!isBoardExists)
                throw new ResponseException(EResponse.NotFound, $"Board with ID {moveTaskDto.NewBoardId} not found.");

            ProjectTask? movedTask = await _taskRepository.GetByIdAsync(moveTaskDto.TaskIdToMove);
            if (movedTask == null)
                throw new ResponseException(EResponse.NotFound, $"Task with ID {moveTaskDto.TaskIdToMove} not found.");

            // === 2. GET NEIGHBOR ORDERS ===
            int? prevOrder = moveTaskDto.PreviousTaskId.HasValue
                ? (await _taskRepository.GetByIdAsync(moveTaskDto.PreviousTaskId.Value))?.Order
                : null;

            int? nextOrder = moveTaskDto.NextTaskId.HasValue
                ? (await _taskRepository.GetByIdAsync(moveTaskDto.NextTaskId.Value))?.Order
                : null;

            int newOrder;

            // === 3. CALCULATE NEW ORDER (The core logic) ===
            if (prevOrder.HasValue && nextOrder.HasValue)
            {
                // Case: Moved between two existing tasks.
                if (prevOrder.Value + 1 >= nextOrder.Value)
                {
                    // COLLISION! There's no integer space between the tasks.
                    // We must shift all subsequent tasks down to make room.

                    // await ShiftTaskOrderRangeAsync(moveTaskDto.NewBoardId, nextOrder.Value, ShiftGap);
                    await ShiftTaskOrderRangeAsync(
                        new()
                        {
                            BoardId = moveTaskDto.NewBoardId,
                            MinOrder = nextOrder.Value,
                            ShiftAmount = ShiftGap,
                        }
                    );

                    // The new position is the midpoint between the previous task and the *new*,
                    // shifted position of the next task.
                    newOrder = (prevOrder.Value + nextOrder.Value + ShiftGap) / 2;
                }
                else
                {
                    // No collision, just find the midpoint.
                    newOrder = (prevOrder.Value + nextOrder.Value) / 2;
                }
            }
            else if (nextOrder.HasValue)
            {
                // Case: Moved to the beginning of the list.
                newOrder = nextOrder.Value - DefaultGap;
            }
            else if (prevOrder.HasValue)
            {
                // Case: Moved to the end of the list.
                newOrder = prevOrder.Value + DefaultGap;
            }
            else
            {
                // Case: Moved to an empty list.
                newOrder = DefaultGap;
            }

            // === 4. APPLY CHANGES AND SAVE ===
            movedTask.BoardId = moveTaskDto.NewBoardId;
            movedTask.Order = newOrder;

            _taskRepository.Update(movedTask); // This should call _context.SaveChangesAsync()

            // If we reached here without errors, commit the transaction.
            await unitOfWork.SaveChangesAsync();
            await unitOfWork.CommitAsync();
        }
        catch (Exception)
        {
            // If any error occurred during the process, roll back all changes.
            await unitOfWork.RollbackAsync();
            throw; // Re-throw the exception to be handled by global error handling.
        }
    }
}

