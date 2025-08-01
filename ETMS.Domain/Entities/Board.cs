using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ETMS.Domain.Entities;

public class Board : BaseEntity
{
    [Required(ErrorMessage = "Board name is required")]
    public string Name { get; set; } = null!;
    public string ColorCode { get; set; } = "#000000";
    public string? Description { get; set; }
    public int ProjectId { get; set; }
    public Project? Project { get; set; }
    public List<ProjectTask>? Tasks { get; set; }
    [ForeignKey("CreatedByUser")]
    public int? CreatedByUserId { get; set; }
    public User? CreatedByUser { get; set; }

    [ForeignKey("UpdatedByUser")]
    public int? UpdatedByUserId { get; set; }
    public User? UpdatedByUser { get; set; }
}