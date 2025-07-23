using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static ETMS.Domain.Enums.Enums;

namespace ETMS.Domain.Entities;

// Renamed from Task to ProjectTask to resolve the conflict between System.Threading.Tasks.Task and Task
public class ProjectTask : BaseEntity
{
    [Required(AllowEmptyStrings = false, ErrorMessage = "Task Name is Required.")]
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    [ForeignKey("Status")]
    public int StatusId { get; set; } = (int)StatusEnum.Pending;
    public Status Status { get; set; } = new();
    [ForeignKey("User")]
    public int? AssignedTo { get; set; }
    public User User { get; set; } = new();
    public List<Comment> Comments { get; set; } = [];
    public List<Attachment> Attachments { get; set; } = [];
}
