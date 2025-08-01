using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ETMS.Domain.Entities;

public class Comment : BaseEntity
{

    [Required(AllowEmptyStrings = false, ErrorMessage = "Comment is Required.")]
    public string CommentString { get; set; } = string.Empty;
    public int? TaskId { get; set; }
    public ProjectTask? Task { get; set; }
    public int? ProjectId { get; set; }
    public Project? Project { get; set; }
    public int UserId { get; set; }
    public User? User { get; set; }

    [ForeignKey("CreatedByUser")]
    public int? CreatedByUserId { get; set; }
    public User? CreatedByUser { get; set; }

    [ForeignKey("UpdatedByUser")]
    public int? UpdatedByUserId { get; set; }
    public User? UpdatedByUser { get; set; }
}