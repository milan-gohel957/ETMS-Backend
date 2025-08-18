using ETMS.Service.DTOs;

namespace ETMS.Service.Services.Interfaces;

public interface ICommentService
{
    Task<CommentDto> CreateCommentAsync(CreateCommentDto createCommentDto);
    Task UpdateCommentAsync(UpdateCommentDto updateCommentDto);
    Task DeleteCommentAsync(int commentId, int userId);
    Task<IEnumerable<CommentDto>> GetCommentsByTaskId(int taskId);
}
