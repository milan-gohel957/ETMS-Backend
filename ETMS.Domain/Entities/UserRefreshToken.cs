using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ETMS.Domain.Entities;

public class UserRefreshToken : BaseEntity
{
    [Required(ErrorMessage = "Guid is Required.")]
    public string Guid { get; set; } = string.Empty;

    public bool IsExpired { get; set; } = false;

    public DateTime ExpiresAt{ get; set; } 
    public int UserId { get; set; }

    public bool IsBlocked { get; set; } = false;

    public string? IpAddress { get; set; }

    //Ignore
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
