using System.ComponentModel.DataAnnotations.Schema;

namespace ETMS.Domain.Entities;

public class ProjectUser
{
    public int Id { get; set; }
    public int ProjectId { get; set; }
    public Project Project { get; set; } = new();
    public int UserId { get; set; } = new();
    public User User { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime? UpdatedAt { get; set; }

  
}

