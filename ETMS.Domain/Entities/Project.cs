using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static ETMS.Domain.Enums.Enums;

namespace ETMS.Domain.Entities;

public class Project : BaseEntity
{

    [Required(AllowEmptyStrings = false, ErrorMessage = "Project Name is Required.")]
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    [ForeignKey("Status")]
    public int StatusId { get; set; } = (int)StatusEnum.Pending;
    public Status? Status { get; set; }
    public List<Comment> Comments { get; set; } = [];
    public List<Attachment> Attachments { get; set; } = [];
    public List<Milestone> Milestones { get; set; } = [];
    public ICollection<UserProjectRole>? UserProjectRoles { get; set; }
    public List<Board> Boards { get; set; } = [];

    [ForeignKey("CreatedByUser")]
    public int? CreatedByUserId { get; set; }
    public User? CreatedByUser { get; set; }

    [ForeignKey("UpdatedByUser")]
    public int? UpdatedByUserId { get; set; }
    public User? UpdatedByUser { get; set; }
}
