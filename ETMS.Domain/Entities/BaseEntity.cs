using System.ComponentModel.DataAnnotations.Schema;
using ETMS.Domain.Entities;

namespace ETMS.Domain.Entities;

public class BaseEntity
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
    public bool IsDeleted { get; set; }
    [ForeignKey("CreatedByUser")]
    public int? CreatedByUserId { get; set; }
    public User? CreatedByUser { get; set; }
    
    [ForeignKey("UpdatedByUser")]
    public int? UpdatedByUserId { get; set; }
    public User? UpdatedByUser { get; set; }
}
