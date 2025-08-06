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
    private IGenericRepository<Board>? _boardRepository;
    private IGenericRepository<Board> BoardRepository => _boardRepository ??= unitOfWork.GetRepository<Board>();

    private readonly IGenericRepository<Project> _projectRepository = unitOfWork.GetRepository<Project>();
    public async Task<BoardDto> GetBoardByIdAsync(int boardId)
    {
        var board = await BoardRepository.GetByIdAsync(boardId);
        if (board == null) throw new ResponseException(EResponse.NotFound, "Board Not Found");
        return mapper.Map<BoardDto>(board);
    }

    public async Task<IEnumerable<BoardDto>> GetBoardsByProjectIdAsync(int projectId)
    {
        var boards = await BoardRepository.GetAllWithIncludesAsync(b => b.ProjectId == projectId, b => b.Tasks.OrderBy(t => t.Order));
        return mapper.Map<IEnumerable<BoardDto>>(boards);
    }

    public async Task<BoardDto> CreateBoardAsync(CreateBoardDto board)
    {
        bool isProjectExists = await _projectRepository.ExistsAsync(board.ProjectId);
        if (!isProjectExists) throw new ResponseException(EResponse.NotFound, "Project Not Found.");

        Board dbBoard = await BoardRepository.AddAsync(mapper.Map<Board>(board));
        await unitOfWork.SaveChangesAsync();
        return mapper.Map<BoardDto>(dbBoard);
    }

    public async Task DeleteBoardAsync(int boardId)
    {
        bool isBoardExists = await BoardRepository.ExistsAsync(boardId);
        if (!isBoardExists) throw new ResponseException(EResponse.NotFound, "Board Not found");

        await BoardRepository.SoftDeleteByIdAsync(boardId);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task UpdateBoardAsync(int boardId, UpdateBoardDto updateBoardDto)
    {
        Board? dbBoard = await BoardRepository.GetByIdAsync(boardId);
        if (dbBoard == null) throw new ResponseException(EResponse.NotFound, "Board Not Found");

        mapper.Map(updateBoardDto, dbBoard);

        BoardRepository.Update(dbBoard);
        await unitOfWork.SaveChangesAsync();
    }
    
    
}