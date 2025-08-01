using System.ComponentModel.DataAnnotations;

public class SignUpRequestDto
{
    [Required]
    [RegularExpression(@"^[a-zA-Z0-9._-]{3,15}$", ErrorMessage = "Username must be between 3 and 15 characters long and can only contain letters, numbers, underscores (_), hyphens (-), and periods (.).")]
    public string Username { get; set; } = string.Empty;

    [Required]
    [MinLength(6)]
    public string Password { get; set; } = string.Empty;

    [Required]
    public string Firstname { get; set; } = string.Empty;

    [Required]
    public string Lastname { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
}