using System.ComponentModel.DataAnnotations;

namespace ETMS.Service.DTOs;

public class RefreshTokenRequestDto
{
    [Required(ErrorMessage = "RefreshToken is Required")]
    public string RefreshToken { get; set; } = string.Empty;
}
