using ETMS.Service.DTOs;

namespace ETMS.Service.Services.Interfaces;

public interface IAuthService
{
    Task<LoginResponseDto> AuthenticateWithGoogleAsync(GoogleLoginDto googleAuthDto);
    Task SignUpAsync(SignUpRequestDto signUpRequestDto, string hostUri);
    Task MagicLoginAsync(string token);
    Task<LoginResponseDto> LoginAsync(LoginRequestDto loginRequestDto);
    Task<LoginResponseDto> RefreshTokenAsync(string refreshToken, string? ipAddress);
    Task<CurrentUserDto> GetCurrentUserDtoAsync(int userId);
}
