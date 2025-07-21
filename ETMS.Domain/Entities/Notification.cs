using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using System.Diagnostics.Contracts;

namespace ETMS.Domain.Entities;

public class Notification : BaseEntity
{

    public int UserId { get; set; }

    public User User { get; set; } = new();

    public string? EntityType { get; set; }

    public int? EntityId { get; set; }

    public NotificationPriority NotificationPriority { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public bool SendEmail { get; set; } = false;

    public bool EmailSent { get; set; } = false;

    public DateTime? EmailSentAt { get; set; }

    public string? EmailError { get; set; }

}

public enum NotificationPriority
{
    Low = 0,
    Normal = 1,
    High = 2,
    Urgent = 3
}
