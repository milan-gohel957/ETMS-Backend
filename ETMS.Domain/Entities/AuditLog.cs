using System.ComponentModel.DataAnnotations.Schema;

namespace ETMS.Domain.Entities;

public class AuditLog
{
    public int Id { get; set; }

    public int? EntityId { get; set; }

    public int? UserId { get; set; }

    public DateTime? TimeStamp { get; set; }

    public string? Action { get; set; }

    public string? TableName { get; set; }

    public string? Changes { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime? UpdatedAt { get; set; }

    // Foreign keys for quick reference (optional but recommended)
    [ForeignKey("CreatedByUser")]
    public int? CreatedByUserId { get; set; }
    public User? CreatedByUser { get; set; }
    [ForeignKey("UpdatedByUser")]
    public int? UpdatedByUserId { get; set; }

    public User? UpdatedByUser { get; set; }
}
