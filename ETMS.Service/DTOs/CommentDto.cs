using System.ComponentModel.DataAnnotations;
using ETMS.Domain.Entities;

namespace ETMS.Service.DTOs;

public class CommentDto : BaseEntityDto
{
    [Required(AllowEmptyStrings = false, ErrorMessage = "Comment is Required.")]
    public string CommentString { get; set; } = string.Empty;
    public int ProjectId { get; set; }
    public int ProjectTaskId { get; set; }
    public int UserId { get; set; }

    public List<CommentMentionDto> Mentions { get; set; } = [];
}

public class CommentMentionDto
{
    public int UserId {get;set;}
    [Required]
    public string? CurrentUserName{ get; set; }
}