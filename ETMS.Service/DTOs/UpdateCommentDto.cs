using System.ComponentModel.DataAnnotations;

namespace ETMS.Service.DTOs;

public class UpdateCommentDto
{
    [Required(AllowEmptyStrings = false, ErrorMessage = "Comment is Required.")]
    public string CommentString { get; set; } = string.Empty;
    public int ProjectTaskId { get; set; }

    public int UserId { get; set; }
    
}
