using ETMS.Domain.Entities;
using ETMS.Service.DTOs;

namespace ETMS.Service.Services.Interfaces;

public interface IBoardService
{
    Task<BoardDto> GetBoardById(int boardId);
    Task<IEnumerable<Board>> GetBoardsByProjectId(int projectId);
    Task CreateBoard(CreateBoardDto board);
    Task UpdateBoard(int boardId, UpdateBoardDto updateBoardDto);
    Task DeleteBoard(int boardId);

}
