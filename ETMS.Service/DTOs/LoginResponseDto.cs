// LoginResponseDto.cs
namespace ETMS.Service.DTOs;

public class LoginResponseDto
{
    public string AccessToken { get; set; } = string.Empty;
    public DateTime AccessExpiresAt { get; set; }

    public string RefreshToken { get; set; } = string.Empty;
    
    public DateTime RefreshTokenExpiresAt { get; set;}
}