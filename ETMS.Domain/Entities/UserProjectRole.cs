namespace ETMS.Domain.Entities;


public class UserProjectRole
{
    public int ProjectId { get; set; }
    public Project Project { get; set; } = null!;

    public int RoleId { get; set; }
    public Role Role { get; set; } = null!;

    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
