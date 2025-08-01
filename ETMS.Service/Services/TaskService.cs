using System.Threading.Tasks;
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
}
