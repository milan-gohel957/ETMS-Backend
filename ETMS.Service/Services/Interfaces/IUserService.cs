using ETMS.Service.DTOs;
using ETMS.Domain.Common;

namespace ETMS.Service.Services.Interfaces;

public interface IUserService
{
    Task<UserNameExistsDto> CheckUserNameExists(string userName);
    Task<PaginatedList<UserDto>> GetPaginatedUsers(int pageIndex = 1,
          int pageSize = 10,
          string sortOrder = "Id asc",
          string? searchString = null,
          int? projectId = null
    );
}
