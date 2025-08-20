using AutoMapper;
using AutoWrapper.Wrappers;
using ETMS.Domain.Entities;
using ETMS.Repository.Repositories.Interfaces;
using ETMS.Service.DTOs;
using ETMS.Service.Exceptions;
using ETMS.Service.Services.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using static ETMS.Domain.Enums.Enums;

namespace ETMS.Service.Services;

public class TaskService(IUnitOfWork unitOfWork, IMapper mapper, IPermissionService permissionService) : ITaskService
{
    IGenericRepository<ProjectTask> _taskRepository = unitOfWork.GetRepository<ProjectTask>();
    IGenericRepository<UserTask> _userTaskRepository = unitOfWork.GetRepository<UserTask>();
    IGenericRepository<Board> _boardRepository = unitOfWork.GetRepository<Board>();
    IGenericRepository<Project> _projectRepository = unitOfWork.GetRepository<Project>();
    IGenericRepository<User> _userRepository = unitOfWork.GetRepository<User>();
    IGenericRepository<UserProjectRole> _userProjectRoleRepository = unitOfWork.GetRepository<UserProjectRole>();
    IPermissionService _permissionService = permissionService;
    public async Task<IEnumerable<TaskDto>> GetTasksByBoardIdAsync(int boardId)
    {
        bool isBoardExists = await _boardRepository.ExistsAsync(boardId);
        if (!isBoardExists)
            throw new ResponseException(EResponse.NotFound, "Board Not Found.");

        return mapper.Map<IEnumerable<TaskDto>>(await _taskRepository.GetAllAsync(t => t.BoardId == boardId));
    }

    public async Task DeleteTask(int boardId, int taskId, int userId)
    {
        bool isBoardExists = await _boardRepository.ExistsAsync(boardId);
        if (!isBoardExists)
            throw new ResponseException(EResponse.NotFound, "Board Not Found.");

        ProjectTask? task = await _taskRepository.FirstOrDefaultAsync(t => t.Id == taskId && t.BoardId == boardId) ?? throw new ResponseException(EResponse.NotFound, $"Task With {taskId} not found in the board with id {boardId}.");

        //Check if user has permission to delete the task
        if (!await _permissionService.HasPermissionAsync(userId: userId, projectId: task.ProjectId, "CanDeleteTask"))
            throw new ResponseException(EResponse.Forbidden, "User is not allowed to perform this action.");

        await _taskRepository.SoftDeleteByIdAsync(taskId);
    }

    public async Task UpdateTaskAsync(int boardId, int taskId, UpdateTaskDto updateTaskDto)
    {
        bool isBoardExists = await _boardRepository.ExistsAsync(boardId);
        if (!isBoardExists)
            throw new ResponseException(EResponse.NotFound, "Board Not Found.");

        ProjectTask? dbTask = await _taskRepository.FirstOrDefaultAsync(t => t.Id == taskId && t.BoardId == boardId);
        if (dbTask == null)
            throw new ResponseException(EResponse.NotFound, "Task Not Found.");
        mapper.Map(updateTaskDto, dbTask);

        _taskRepository.Update(dbTask);

        await unitOfWork.SaveChangesAsync();
    }
    public async Task<TaskDto> CreateTaskAsync(CreateTaskDto createTaskDto)
    {
        bool isProjectExist = await _projectRepository.ExistsAsync(createTaskDto.ProjectId);
        if (!isProjectExist)
            throw new ResponseException(EResponse.NotFound, $"Project With {createTaskDto.ProjectId} Not found");

        bool isBoardExists = await _boardRepository.ExistsAsync(createTaskDto.BoardId);

        if (!isBoardExists)
            throw new ResponseException(EResponse.NotFound, $"Board with {createTaskDto.BoardId} Not Found");

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

    public async Task<IEnumerable<UserDto>> GetTaskMembersAsync(int taskId)
    {
        bool isTaskExists = await _taskRepository.ExistsAsync(taskId);
        if (!isTaskExists)
            throw new ResponseException(EResponse.NotFound, $"Task With {taskId} not found.");

        IEnumerable<User> taskMembers = (await _userTaskRepository.GetAllWithIncludesAsync(ut => ut.ProjectTaskId == taskId, includes: ut => ut.User)).Select(ut => ut.User);

        return mapper.Map<IEnumerable<UserDto>>(taskMembers);
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
    public async Task MoveTaskAsync(MoveTaskDto moveTaskDto, int taskId)
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

            ProjectTask? movedTask = await _taskRepository.GetByIdAsync(taskId);
            if (movedTask == null)
                throw new ResponseException(EResponse.NotFound, $"Task with ID {taskId} not found.");

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
    public async Task AssignTaskToUsers(int taskId, AssignTaskUserDto assignUsersToTaskDto)
    {
        // First, validate if the task exists.
        ProjectTask task = await _taskRepository.GetByIdAsync(taskId)
            ?? throw new ResponseException(EResponse.NotFound, $"Task with ID {taskId} not found.");

        bool allUsersExist = assignUsersToTaskDto.UserIds.All(id => _userRepository.Table.Any(u => u.Id == id));
        if (!allUsersExist)
            throw new ResponseException(EResponse.NotFound, "Some of the Users Doesn't exists");

        // IMPORTANT: Check if the users belong to the task's project.
        // Assuming you have a way to query UserProjectRole table to check for this relationship.
        var projectUsers = (await _userProjectRoleRepository.GetAllAsync(upr => upr.ProjectId == task.ProjectId)).Select(upr => upr.UserId);

        var userIdsToAssign = assignUsersToTaskDto.UserIds.ToList();
        var invalidUserIds = userIdsToAssign.Except(projectUsers).ToList();

        if (invalidUserIds.Any())
        {
            throw new ResponseException(EResponse.BadRequest,
                $"The following user(s) are not members of the project: {string.Join(", ", invalidUserIds)}.");
        }

        // Check if the users being assigned actually exist in the system.
        var existingUsers = await _userRepository.Table.Where(u => userIdsToAssign.Contains(u.Id)).ToListAsync();

        if (existingUsers.Count != userIdsToAssign.Count)
        {
            var missingUserIds = userIdsToAssign.Except(existingUsers.Select(u => u.Id));
            throw new ResponseException(EResponse.BadRequest, $"The following user(s) could not be found: {string.Join(", ", missingUserIds)}.");
        }

        // Create all UserTask entities from the DTO.
        var newAssignments = userIdsToAssign
            .Select(userId => new UserTask
            {
                ProjectTaskId = taskId,
                UserId = userId
            })
            .ToList();

        try
        {
            await _userTaskRepository.AddRangeAsync(newAssignments);
            await unitOfWork.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            if (ex.InnerException is SqlException sqlEx && sqlEx.Number == 2627)
            {
                throw new ResponseException(EResponse.Conflict, "One or more users are already assigned to this task.");
            }
            throw;
        }
    }
}

