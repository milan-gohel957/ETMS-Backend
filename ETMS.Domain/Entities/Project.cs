using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static ETMS.Domain.Enums.Enums;

namespace ETMS.Domain.Entities;

public class Project : BaseEntity
{

    [Required(AllowEmptyStrings = false, ErrorMessage = "Project Name is Required.")]
    [RegularExpression(@"\S+", ErrorMessage = "Project Name cannot be empty or whitespace.")]
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    [ForeignKey("Status")]
    public int StatusId { get; set; } = (int)StatusEnum.Pending;
    public Status Status { get; set; } = new();

    public List<Comment> Comments { get; set; } = [];
    public List<Attachment> Attachments { get; set; } = [];

    public List<Milestone> Milestones { get; set; } = [];

    public ICollection<UserProjectRole>? UserProjectRoles { get; set; }
}
