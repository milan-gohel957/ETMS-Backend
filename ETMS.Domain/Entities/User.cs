using System.ComponentModel.DataAnnotations;

namespace ETMS.Domain.Entities;

public class User : BaseEntity
{
    [Required, MaxLength(50)]
    public string FirstName { get; set; } = string.Empty;

    [Required, MaxLength(50)]
    public string LastName { get; set; } = string.Empty;

    [MaxLength(100)]
    [RegularExpression(@"^[a-zA-Z0-9._-]{3,15}$", ErrorMessage = "Username must be between 3 and 15 characters long and can only contain letters, numbers, underscores (_), hyphens (-), and periods (.).")]
    public string UserName { get; set; } = null!;

    [Required, EmailAddress, MaxLength(100)]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string PasswordHash { get; set; } = string.Empty;

    [MaxLength(200)]
    public string? AvatarUrl { get; set; }

    [MaxLength(30)]
    public string? Phone { get; set; }
    public bool IsActive { get; set; } = true;
    public bool IsEmailConfirmed { get; set; } = false;
    public string? ResetPasswordToken { get; set; }
    public DateTime? ResetPasswordTokenExpiry { get; set; }
    public List<Comment> Comments { get; set; } = [];
    public List<ProjectTask> AssignedTasks { get; set; } = [];
    public ICollection<UserProjectRole>? UserProjectRoles { get; set; }
    public bool IsVerifiedUser { get; set; } = false;
    public string? MagicLinkToken { get; set; }
    public DateTime? MagicLinkTokenExpiry { get; set; }
    public virtual ICollection<UserRole> UserRoles { get; set; } = null!;
    public List<UserTask> UserTasks { get; set; } = [];
}