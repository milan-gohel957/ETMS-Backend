using System.ComponentModel.DataAnnotations.Schema;

namespace ETMS.Domain.Entities;

public class UserRole : BaseEntity
{
    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public int RoleId { get; set; }
    public Role Role { get; set; } = null!;

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
