using ETMS.Domain.Entities;
using ETMS.Service.DTOs;

namespace ETMS.Service.Services.Interfaces;

public interface IBoardService
{
    Task<BoardDto> GetBoardByIdAsync(int boardId);
    Task<IEnumerable<BoardDto>> GetBoardsByProjectIdAsync(int projectId);
    Task<BoardDto> CreateBoardAsync(CreateBoardDto board);
    Task UpdateBoardAsync(int boardId, UpdateBoardDto updateBoardDto);
    Task DeleteBoardAsync(int boardId);
    Task MoveBoardAsync(int boardId, MoveBoardDto moveBoardDto);

}
