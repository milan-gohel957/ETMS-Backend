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
public class BoardController(IBoardService boardService) : ControllerBase
{
    [HttpGet("by-project/{projectId:int}")]
    [HandleRequestResponse(TypeResponse = ETypeRequestResponse.ResponseWithData)]
    public async Task<Response<IEnumerable<BoardDto>>> GetBoardsByProjectId(int projectId)
    {
        IEnumerable<BoardDto> boards = await boardService.GetBoardsByProjectIdAsync(projectId);
        return new()
        {
            Data = boards,
            Message = "Boards retrieved successfully.",
            Succeeded = true,
            StatusCode = HttpStatusCode.OK
        };
    }

    [HttpPost]
    [HandleRequestResponse(TypeResponse = ETypeRequestResponse.ResponseWithData)]
    public async Task<Response<BoardDto>> CreateBoard(CreateBoardDto createBoardDto)
    {
        BoardDto createdBoardDto = await boardService.CreateBoardAsync(createBoardDto);
        return new()
        {
            Data = createdBoardDto,
            Message = "Board created successfully.",
            Succeeded = true,
            StatusCode = HttpStatusCode.Created // 201 Created is more semantic here
        };
    }

    [HttpGet("{id}")]
    [HandleRequestResponse(TypeResponse = ETypeRequestResponse.ResponseWithData)]
    public async Task<Response<BoardDto>> GetBoardById(int id)
    {
        var board = await boardService.GetBoardByIdAsync(id);
        return new()
        {
            Data = board,
            Message = "Board retrieved successfully.",
            Succeeded = true,
            StatusCode = HttpStatusCode.OK
        };
    }

    [HttpPut("{id:int}")]
    [HandleRequestResponse]
    public async Task<Response<object>> UpdateBoard(int id, UpdateBoardDto updateBoardDto)
    {
        await boardService.UpdateBoardAsync(id, updateBoardDto);
        return new()
        {
            Data = null,
            Message = "Board updated successfully.",
            Succeeded = true,
            StatusCode = HttpStatusCode.OK
        };
    }

    [HttpDelete("{id:int}")]
    [HandleRequestResponse]
    public async Task<Response<object>> DeleteBoard(int id)
    {
        await boardService.DeleteBoardAsync(id);
        return new()
        {
            Data = null,
            Message = "Board deleted successfully.",
            Succeeded = true,
            StatusCode = HttpStatusCode.OK
        };
    }
}