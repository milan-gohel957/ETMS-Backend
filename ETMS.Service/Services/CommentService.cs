using System.Text.RegularExpressions;
using AutoMapper;
using ETMS.Domain.Entities;
using ETMS.Repository.Repositories.Interfaces;
using ETMS.Service.DTOs;
using ETMS.Service.Exceptions;
using ETMS.Service.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using static ETMS.Domain.Enums.Enums;

namespace ETMS.Service.Services;

public class CommentService(IUnitOfWork unitOfWork, IMapper mapper, IPermissionService permissionService) : ICommentService
{
    private readonly IGenericRepository<Comment> _commentRepository = unitOfWork.GetRepository<Comment>();
    private readonly IGenericRepository<CommentMention> _commentMentionsRepository = unitOfWork.GetRepository<CommentMention>();
    private readonly IGenericRepository<ProjectTask> _taskRepository = unitOfWork.GetRepository<ProjectTask>();
    private readonly IGenericRepository<User> _userRepository = unitOfWork.GetRepository<User>();
    private readonly IPermissionService _permissionService = permissionService;

    public async Task<CommentDto> CreateCommentAsync(CreateCommentDto createCommentDto)
    {
        //TODO: Set UserId from controller
        CommentDto commentDto = new();
        ProjectTask? dbTask = await _taskRepository.GetByIdAsync(createCommentDto.ProjectTaskId) ?? throw new ResponseException(EResponse.NotFound, $"Task With {createCommentDto.ProjectTaskId} not found.");

        bool canCreateComment = await _permissionService.HasPermissionAsync(
            projectId: dbTask.ProjectId,
            userId: createCommentDto.UserId,
            permission: "CanCreateComment"
        );

        if (!canCreateComment)
            throw new ResponseException(EResponse.Forbidden, "User Can't Add Comment in this Project.");

        var comment = mapper.Map<Comment>(createCommentDto);

        comment.ProjectId = dbTask.ProjectId;

        Comment addedComment = await _commentRepository.AddAsync(comment);
        await unitOfWork.SaveChangesAsync();

        List<string> mentions = FindMentions(createCommentDto.CommentString);

        for (int i = 0; i < mentions.Count; i++)
        {
            User? user = await _userRepository.FirstOrDefaultAsync(y => y.UserName == mentions[i].Substring(1));
            if (user == null) continue;
            CommentMention commentMention = new()
            {
                UserId = user.Id,
                CommentId = addedComment.Id,
            };

            commentDto.Mentions.Add(new CommentMentionDto()
            {
                CurrentUserName = user.UserName,
                UserId = user.Id
            });

            await _commentMentionsRepository.AddAsync(commentMention);
        }
        await unitOfWork.SaveChangesAsync();

        mapper.Map(addedComment, commentDto);

        return commentDto;
    }

    private static List<string> FindMentions(string text)
    {
        List<string> mentions = new List<string>();
        string pattern = @"\B@\w+";
        MatchCollection matchCollection = Regex.Matches(text, pattern);

        foreach (Match match in matchCollection)
        {
            mentions.Add(match.Value);
        }

        return mentions;
    }

    public async Task UpdateCommentAsync(UpdateCommentDto updateCommentDto)
    {
        //TODO: Set UserId from controller
        ProjectTask? dbTask = await _taskRepository.GetByIdAsync(updateCommentDto.ProjectTaskId) ?? throw new ResponseException(EResponse.NotFound, $"Task With {updateCommentDto.ProjectTaskId} not found.");

        Comment? dbComment = await _commentRepository.FirstOrDefaultAsync(c => c.Id == updateCommentDto.Id && c.UserId == updateCommentDto.UserId)
                            ?? throw new ResponseException(EResponse.Forbidden, $"User with {updateCommentDto.UserId} can not update this comment.");

        mapper.Map(updateCommentDto, dbComment);

        List<string> mentions = FindMentions(updateCommentDto.CommentString);

        for (int i = 0; i < mentions.Count; i++)
        {
            User? user = await _userRepository.FirstOrDefaultAsync(y => y.UserName == mentions[i]);
            if (user == null) continue;
            CommentMention commentMention = new()
            {
                UserId = user.Id,
                CommentId = updateCommentDto.Id,
            };

            await _commentMentionsRepository.AddAsync(commentMention);
        }

        _commentRepository.Update(dbComment);

        await unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteCommentAsync(int commentId, int userId)
    {
        bool isCommentExists = await _commentRepository.AnyAsync(c => c.Id == commentId && c.UserId == userId);

        if (!isCommentExists)
        {
            throw new ResponseException(EResponse.NotFound, "Comment not found or user does not have permission to delete.");
        }

        await _commentRepository.SoftDeleteByIdAsync(commentId);

        await unitOfWork.SaveChangesAsync();
    }
    public async Task<IEnumerable<CommentDto>> GetCommentsByTaskId(int taskId)
    {
        var commentsTable = _commentRepository.Table;

        IEnumerable<Comment> comments = await commentsTable.Include(c => c.CommentMentions!).ThenInclude(cm => cm.User).Where(c => c.ProjectTaskId == taskId).ToListAsync();

        return mapper.Map<IEnumerable<CommentDto>>(comments);
    }
}
