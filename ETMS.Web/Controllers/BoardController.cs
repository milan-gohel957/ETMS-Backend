using System.Net;
using ETMS.Service.DTOs;
using ETMS.Service.Services.Interfaces;
using ETMS.Web.Filters;
using Microsoft.AspNetCore.Mvc;
using static ETMS.Domain.Enums.Enums;
using ETMS.Domain.Common;

namespace ETMS.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BoardController(IBoardService boardService) : BaseApiController
{
    [HttpGet("by-project/{projectId:int}")]
    public async Task<IActionResult> GetBoardsByProjectId(int projectId)
    {
        IEnumerable<BoardDto> boards = await boardService.GetBoardsByProjectIdAsync(projectId);
        return Success(boards);
    }

    [HttpPost]
    public async Task<IActionResult> CreateBoard(CreateBoardDto createBoardDto)
    {
        BoardDto createdBoardDto = await boardService.CreateBoardAsync(createBoardDto);
        return Success(createdBoardDto);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBoardById(int id)
    {
        BoardDto board = await boardService.GetBoardByIdAsync(id);
        return Success(board);
    }

    [HttpPatch("move/{boardId:int}")]
    public async Task<IActionResult> MoveBoard(int boardId, MoveBoardDto moveBoardDto)
    {
        await boardService.MoveBoardAsync(boardId, moveBoardDto);
        return Success<object>(null, "Board moved successfully.");
    }
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateBoard(int id, UpdateBoardDto updateBoardDto)
    {
        await boardService.UpdateBoardAsync(id, updateBoardDto);
        return Success<object>(null, "Board updated successfully.");
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteBoard(int id)
    {
        await boardService.DeleteBoardAsync(id);
        return Success<object>(null, "Board deleted successfully.");
    }
}