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
}
