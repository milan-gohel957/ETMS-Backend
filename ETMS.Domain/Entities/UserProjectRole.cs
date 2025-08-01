using System.ComponentModel.DataAnnotations.Schema;

namespace ETMS.Domain.Entities;


public class UserProjectRole : BaseEntity
{
    public int ProjectId { get; set; }
    public Project? Project { get; set; } 

    public int RoleId { get; set; }
    public Role? Role { get; set; } 

    public int UserId { get; set; }
    public User? User { get; set; } 

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
