using System.Security.Claims;
using ETMS.Domain.Entities;

namespace ETMS.Service.Services.Interfaces;

public interface ITokenService
{
    Task<(string token, DateTime expiresAt)> GenerateAccessToken(User user);
    (string token, DateTime expiresAt, string guid) GenerateRefreshToken();
    ClaimsPrincipal ValidateAndDecodeToken(string token);
}
