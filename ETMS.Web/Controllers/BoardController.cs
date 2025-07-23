using ETMS.Domain.Entities;
using ETMS.Service.DTOs;
using ETMS.Service.Services.Interfaces;
using ETMS.Web.Filters;
using Microsoft.AspNetCore.Mvc;
using static ETMS.Domain.Enums.Enums;

namespace ETMS.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BoardController(IBoardService boardService) : ControllerBase
{
    [HttpGet("by-project/{projectId:int}")]
    [HandleRequestResponse(TypeResponse = ETypeRequestResponse.ResponseWithData)]
    public async Task<ActionResult<IEnumerable<BoardDto>>> GetBoardsByProjectId(int projectId)
    {
        IEnumerable<BoardDto> boards = await boardService.GetBoardsByProjectIdAsync(projectId);
        return Ok(boards);
    }

    [HttpPost]
    [HandleRequestResponse]
    public async Task<IActionResult> CreateBoard(CreateBoardDto createBoardDto)
    {
        BoardDto createdBoardDto = await boardService.CreateBoardAsync(createBoardDto);
        return CreatedAtAction(
            nameof(GetBoardById),
            new { id = createdBoardDto.Id },
            createdBoardDto
        );
    }

    [HttpGet("{id}")]
    [HandleRequestResponse(TypeResponse = ETypeRequestResponse.ResponseWithData)]
    public async Task<ActionResult<BoardDto>> GetBoardById(int id)
    {
        return Ok(await boardService.GetBoardByIdAsync(id));
    }

    [HttpPut("{id:int}")]
    [HandleRequestResponse]
    public async Task<IActionResult> UpdateBoard(int id, UpdateBoardDto updateBoardDto)
    {
        await boardService.UpdateBoardAsync(id, updateBoardDto);
        return NoContent();
    }
    [HttpDelete("{id:int}")]
    [HandleRequestResponse]
    public async Task<IActionResult> DeleteBoard(int id)
    {
        await boardService.DeleteBoardAsync(id);
        return NoContent();
    }

}

