using ETMS.Domain.Entities;
using ETMS.Repository.Repositories.Interfaces;
using ETMS.Service.DTOs;
using ETMS.Service.Exceptions;
using ETMS.Service.Services.Interfaces;

namespace ETMS.Service.Services;

public class UserService(IUnitOfWork unitOfWork) : IUserService
{
    private readonly IGenericRepository<User> _userRepository = unitOfWork.GetRepository<User>();
    public async Task<UserNameExistsDto> CheckUserNameExists(string userName)
    {
        if (string.IsNullOrEmpty(userName))
            throw new ResponseException(Domain.Enums.Enums.EResponse.BadRequest, "UserName is Required");

        bool isUserNameExists = await _userRepository.AnyAsync(x => x.UserName == userName);

        return new()
        {
            IsUserNameExists = isUserNameExists
        };
    }
}
