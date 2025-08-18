using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ETMS.Domain.Entities;

[Index(nameof(UserId), nameof(ProjectTaskId), IsUnique = true)]
public class UserTask : BaseEntity
{
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public int ProjectTaskId { get; set; }
    public ProjectTask ProjectTask { get; set; } = null!;
    [NotMapped]
    [ForeignKey("CreatedByUser")]
    public int? CreatedByUserId { get; set; }
    [NotMapped]

    public User? CreatedByUser { get; set; }
    [NotMapped]

    [ForeignKey("UpdatedByUser")]
    public int? UpdatedByUserId { get; set; }
    [NotMapped]
    public User? UpdatedByUser { get; set; }
}
