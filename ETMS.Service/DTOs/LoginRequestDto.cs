using System.ComponentModel.DataAnnotations;

namespace ETMS.Service.DTOs;

public class LoginRequestDto
{
    [Required(ErrorMessage = "Email is Required")]
    [RegularExpression(@"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$",
        ErrorMessage = "Invalid email address.")]
    public string Email { get; set; } = string.Empty;
    [Required(ErrorMessage = "Password is Required")]
    public string Password { get; set; } = string.Empty;

    public string? UserHostAddress { get; set; }
}
