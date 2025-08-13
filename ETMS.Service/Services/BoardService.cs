using AutoMapper;
using ETMS.Domain.Entities;
using ETMS.Repository.Repositories.Interfaces;
using ETMS.Service.DTOs;
using ETMS.Service.Exceptions;
using ETMS.Service.Services.Interfaces;
using static ETMS.Domain.Enums.Enums;

namespace ETMS.Service.Services;

public class BoardService(IUnitOfWork unitOfWork, IMapper mapper) : IBoardService
{
    private IGenericRepository<Board> _boardRepository = unitOfWork.GetRepository<Board>();
    private IGenericRepository<ProjectTask> _taskRepository = unitOfWork.GetRepository<ProjectTask>();
    private IGenericRepository<UserProjectRole> _userProjectRoleRepository = unitOfWork.GetRepository<UserProjectRole>();

    private readonly IGenericRepository<Project> _projectRepository = unitOfWork.GetRepository<Project>();
    public async Task<BoardDto> GetBoardByIdAsync(int boardId)
    {
        var board = await _boardRepository.GetByIdAsync(boardId);
        if (board == null) throw new ResponseException(EResponse.NotFound, "Board Not Found");
        return mapper.Map<BoardDto>(board);
    }

    public async Task MoveBoardAsync(int boardId, MoveBoardDto moveBoardDto)
    {
        Board? board = await _boardRepository.GetByIdAsync(boardId);
        if (board == null) throw new ResponseException(EResponse.NotFound, $"Board With {boardId} Not Found");

        Board? prevBoard = moveBoardDto.PreviousBoardId.HasValue ? await _boardRepository.GetByIdAsync(moveBoardDto.PreviousBoardId.Value) : null;
        Board? nextBoard = moveBoardDto.NextBoardId.HasValue ? await _boardRepository.GetByIdAsync(moveBoardDto.NextBoardId.Value) : null;

        if ((prevBoard != null && prevBoard.ProjectId != board.ProjectId) || (nextBoard != null && nextBoard.ProjectId != board.ProjectId))
        {
            throw new ResponseException(EResponse.BadRequest, "Previous Board and Next Board must belong to the same project.");
        }

        int? prevBoardOrder = prevBoard?.Order;
        int? nextBoardOrder = nextBoard?.Order;

        if (prevBoardOrder != null && nextBoardOrder != null)
        {
            board.Order = prevBoardOrder.Value + (nextBoardOrder.Value - prevBoardOrder.Value) / 2;
        }
        else if (prevBoardOrder == null && nextBoardOrder != null)
        {
            board.Order = nextBoardOrder.Value - 100;
        }
        else if (prevBoardOrder != null && nextBoardOrder == null)
        {
            board.Order = prevBoardOrder.Value + 100;
        }
        else
        {
            board.Order = 100;
        }
        _boardRepository.Update(board);
        await unitOfWork.SaveChangesAsync();
    }
    public async Task<IEnumerable<BoardDto>> GetBoardsByProjectIdAsync(int projectId)
    {
        var boards = await _boardRepository.GetAllWithIncludesAsync(b => b.ProjectId == projectId, includes:b => b.Tasks.OrderBy(t => t.Order), orderBy:b => b.OrderBy(b => b.Order));
        return mapper.Map<IEnumerable<BoardDto>>(boards);
    }

    public async Task<BoardDto> CreateBoardAsync(CreateBoardDto board)
    {
        bool isProjectExists = await _projectRepository.ExistsAsync(board.ProjectId);
        if (!isProjectExists) throw new ResponseException(EResponse.NotFound, "Project Not Found.");

        Board dbBoard = await _boardRepository.AddAsync(mapper.Map<Board>(board));
        await unitOfWork.SaveChangesAsync();
        return mapper.Map<BoardDto>(dbBoard);
    }

    public async Task DeleteBoardAsync(int boardId)
    {
        bool isBoardExists = await _boardRepository.ExistsAsync(boardId);
        if (!isBoardExists) throw new ResponseException(EResponse.NotFound, "Board Not found");

        await _boardRepository.SoftDeleteByIdAsync(boardId);
        //Delete all tasks associated with the board
        await _taskRepository.SoftDeleteRangeAsync(t => t.BoardId == boardId);

        await unitOfWork.SaveChangesAsync();
    }

    public async Task UpdateBoardAsync(int boardId, UpdateBoardDto updateBoardDto)
    {
        Board? dbBoard = await _boardRepository.GetByIdAsync(boardId);
        if (dbBoard == null) throw new ResponseException(EResponse.NotFound, "Board Not Found");

        mapper.Map(updateBoardDto, dbBoard);

        _boardRepository.Update(dbBoard);
        await unitOfWork.SaveChangesAsync();
    }


}