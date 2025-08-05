using ETMS.Service.DTOs;

namespace ETMS.Service.Services.Interfaces;

public interface IUserService
{
    Task<UserNameExistsDto> CheckUserNameExists(string userName);

}
