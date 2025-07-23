using ETMS.Domain.Entities;

namespace ETMS.Service.Services.Interfaces;

public interface ITokenService
{
    string GenerateJwtToken(User user);

}
