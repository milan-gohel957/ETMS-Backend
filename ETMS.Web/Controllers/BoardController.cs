using ETMS.Domain.Entities;
using ETMS.Service.DTOs;
using ETMS.Service.Services.Interfaces;
using ETMS.Web.Filters;
using Microsoft.AspNetCore.Mvc;

namespace ETMS.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BoardController(IBoardService boardService) : ControllerBase
{
    [HttpGet("{projectId}")]
    [HandleRequestResponse(TypeResponse = Domain.Enums.Enums.ETypeRequestResponse.ResponseWithData)]

    public async Task<IActionResult> GetBoardsByProjectId(int projectId)
    {
        IEnumerable<Board> boards = await boardService.GetBoardsByProjectId(projectId);
        return Ok(boards);
    }

    [HttpPost]
    [HandleRequestResponse]
    public async Task<IActionResult> CreateBoard(CreateBoardDto createBoardDto)
    {
        await boardService.CreateBoard(createBoardDto);
        return Ok("Board Created.");

    }
    [HttpPut("{boardId}")]
    [HandleRequestResponse]
    public async Task<IActionResult> UpdateBoard(int boardId, UpdateBoardDto updateBoardDto)
    {
        await boardService.UpdateBoard(boardId, updateBoardDto);
        return Ok("Board Updated!");
    }
    [HttpDelete("{boardId}")]
    [HandleRequestResponse]
    public async Task<IActionResult> DeleteBoard(int boardId)
    {
        await boardService.DeleteBoard(boardId);
        return Ok("Board Deleted!");
    }

}

