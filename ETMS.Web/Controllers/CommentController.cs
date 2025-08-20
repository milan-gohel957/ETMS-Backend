using System.Security.Claims;
using ETMS.Service.DTOs;
using ETMS.Service.Exceptions;
using ETMS.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static ETMS.Domain.Enums.Enums;

namespace ETMS.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CommentController(ICommentService commentService) : BaseApiController
{
    [HttpGet("tasks/{taskId:int}/comments")]
    public async Task<IActionResult> GetCommentsByTaskId(int taskId)
    {
        IEnumerable<CommentDto> commentDtos = await commentService.GetCommentsByTaskId(taskId);
        return Success(commentDtos);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCommentAsync(CreateCommentDto createCommentDto)
    {
        createCommentDto.UserId = GetCurrentUserId();
        CommentDto commentDto = await commentService.CreateCommentAsync(createCommentDto);
        return Success(commentDto);
    }

    [HttpPut("{commentId:int}")]
    public async Task<IActionResult> UpdateCommentAsync(int commentId, UpdateCommentDto updateCommentDto)
    {
        updateCommentDto.UserId = GetCurrentUserId();
        await commentService.UpdateCommentAsync(commentId, updateCommentDto);
        return Success("Comment Updated SuccessFully!");
    }

    [HttpDelete("{commentId:int}")]
    public async Task<IActionResult> DeleteCommentAsync(int commentId)
    {
        int userId = GetCurrentUserId();
        await commentService.DeleteCommentAsync(commentId, userId);

        return Success("Comment Deleted!");
    }
    private int GetCurrentUserId()
    {
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userIdString) || !int.TryParse(userIdString, out var userId))
        {
            // Throwing an exception is better here, as your filter will handle it
            throw new ResponseException(EResponse.Unauthorized, "Invalid user identifier in token.");
        }

        return userId;
    }
}
