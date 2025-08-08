using static ETMS.Domain.Enums.Enums;

namespace ETMS.Service.DTOs;

public class GoogleLoginDto
{
    public string AccessToken { get; set; } = "";
    public string IdToken { get; set; } = "";
    public string? IpAddress { get; set; }
    
    public AuthProviderEnum AuthProviderEnum = AuthProviderEnum.Google;
}
