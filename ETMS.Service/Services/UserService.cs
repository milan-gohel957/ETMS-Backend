using AutoMapper;
using ETMS.Domain.Common;
using ETMS.Domain.Entities;
using ETMS.Repository.Repositories.Interfaces;
using ETMS.Service.DTOs;
using ETMS.Service.Exceptions;
using ETMS.Service.Services.Interfaces;

namespace ETMS.Service.Services;

public class UserService(IUnitOfWork unitOfWork, IMapper mapper) : IUserService
{
    private readonly IGenericRepository<User> _userRepository = unitOfWork.GetRepository<User>();
    private readonly IGenericRepository<UserProjectRole> _userProjectRoleRepository = unitOfWork.GetRepository<UserProjectRole>();
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

    public async Task<PaginatedList<UserDto>> GetPaginatedUsers(int pageIndex = 1,
            int pageSize = 10,
            string sortOrder = "Id asc",
            string? searchString = null,
            int? projectId = null
    )
    {
        var source = _userRepository.Table;

        // Define the search predicate based on the search string
        System.Linq.Expressions.Expression<Func<User, bool>>? searchPredicate = null;
        if (!string.IsNullOrEmpty(searchString))
        {
            searchPredicate = p => p.FirstName.Contains(searchString)
            || p.LastName.Contains(searchString)
            || p.UserName.Contains(searchString)
            || p.Email.Contains(searchString);
        }

        // Create the paginated list
        var paginatedList = await PaginatedList<User>.CreateAsync(
            source,
            pageIndex,
            pageSize,
            sortOrder,
            searchPredicate);


        var userListDto = mapper.Map<List<UserDto>>(paginatedList.Items);

        var paginatedDtoList = new PaginatedList<UserDto>(
                userListDto,
                paginatedList.TotalCount,
                paginatedList.PageIndex,
                pageSize // Pass the original pageSize used for the query
            );

        if (projectId != null)
        {
            var projectUsers = (await _userProjectRoleRepository
                          .GetAllAsync(upr => upr.ProjectId == projectId))
                          .Select(upr => upr.UserId)
                          .ToHashSet(); // HashSet for O(1) lookup

            // Mark each user with InProject = true/false
            foreach (var user in userListDto)
            {
                user.InProject = projectUsers.Contains(user.Id);
            }
        }

        return paginatedDtoList;
    }
}
