using ETMS.Domain.Common;
using ETMS.Service.DTOs;
using ETMS.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ETMS.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(IUserService userService) : BaseApiController
{
    [HttpGet("exists")]
    public async Task<IActionResult> CheckUserNameExists([FromQuery] string userName)
    {
        UserNameExistsDto isUserNameExists = await userService.CheckUserNameExists(userName);

        return Success(isUserNameExists, isUserNameExists.IsUserNameExists ? "Username already Exists." : "Username doesn't exists.");
    }

    [HttpGet]
    public async Task<IActionResult> GetPaginatedUsers(int pageIndex = 1,
            int pageSize = 10,
            string sortOrder = "Id asc",
            string? searchString = null)
    {
        return Success(await userService.GetPaginatedUsers(pageIndex, pageSize, sortOrder, searchString));
    }
    [HttpGet("search/members")]
    public async Task<IActionResult> SearchUsers([FromQuery]string searchString, [FromQuery]int? projectId)
    {
        // Don't search for string length less than 3
        if (string.IsNullOrEmpty(searchString) || searchString.Length < 3)
        {
            return Success(new UserDto[] { });
        }

        return Success(await userService.GetPaginatedUsers(pageIndex: 1, pageSize: 10, sortOrder: "Id asc", searchString, projectId));
    }
}
