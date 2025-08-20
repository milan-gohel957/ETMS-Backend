using System.ComponentModel.DataAnnotations;

namespace ETMS.Service.DTOs;

public class UserDto : BaseEntityDto
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

    [MaxLength(200)]
    public string? AvatarUrl { get; set; }
    public int RoleId{ get; set; }
    public bool InProject { get; set; } = false;
    
}
