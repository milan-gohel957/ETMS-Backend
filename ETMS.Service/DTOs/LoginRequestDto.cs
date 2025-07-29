using System.ComponentModel.DataAnnotations;

namespace ETMS.Service.DTOs;

public class LoginRequestDto
{
    [Required(ErrorMessage = "Email is Required")]
    public string Email { get; set; } = string.Empty;
    [Required(ErrorMessage = "Password is Required")]
    public string Password { get; set; } = string.Empty;

    public string? UserHostAddress{ get; set; }
}
