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

    public async Task<BoardDto> GetBoardById(int boardId)
    {
        return mapper.Map<BoardDto>(await BoardRepository.GetByIdAsync(boardId));
    }

    public async Task<IEnumerable<Board>> GetBoardsByProjectId(int projectId)
    {
        return mapper.Map<IEnumerable<Board>>(await BoardRepository.GetAllAsync(b => b.Equals(projectId)));
    }

    public async Task CreateBoard(CreateBoardDto board)
    {
        await BoardRepository.AddAsync(mapper.Map<Board>(board));
        await unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteBoard(int boardId)
    {
        await BoardRepository.SoftDeleteByIdAsync(boardId);
        await unitOfWork.SaveChangesAsync();
    }


    public async Task UpdateBoard(int boardId, UpdateBoardDto updateBoardDto)
    {
        Board? dbBoard = await BoardRepository.GetByIdAsync(boardId);
        if (dbBoard == null) throw new ResponseException(EResponse.NotFound, "Board Not Found");

        mapper.Map(updateBoardDto, dbBoard);

        BoardRepository.Update(dbBoard);
        await unitOfWork.SaveChangesAsync();
    }
}