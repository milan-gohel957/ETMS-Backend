using ETMS.Domain.DTOs;

namespace ETMS.Service.Services.Interfaces;

public interface IAuthService
{
    Task SignUpAsync(SignUpRequestDto signUpRequestDto, string hostUri);
    Task MagicLoginAsync(string token);
    Task<string> LoginAsync(LoginRequestDto loginRequestDto);

}
