using ETMS.Domain.Common;
using ETMS.Service.DTOs;
using ETMS.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ETMS.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(IUserService userService) : ControllerBase
{
    [HttpGet("exists")]
    public async Task<Response<UserNameExistsDto>> CheckUserNameExists([FromQuery]string userName)
    {
        UserNameExistsDto isUserNameExists = await userService.CheckUserNameExists(userName);
        return new Response<UserNameExistsDto>()
        {
            Data = isUserNameExists,
            Succeeded = true,
            Errors = [],
            Message = isUserNameExists.IsUserNameExists ? "Username already Exists." : "Username doesn't exists.",
            StatusCode = System.Net.HttpStatusCode.OK
        };
    }
}
