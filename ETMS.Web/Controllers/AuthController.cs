using ETMS.Service.DTOs;
using ETMS.Service.Services.Interfaces;
using ETMS.Web.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ETMS.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost("signup")]
    [HandleRequestResponse]
    public async Task<IActionResult> SignUp([FromBody] SignUpRequestDto signUpRequestDto)
    {
        var hostUri = $"{Request.Scheme}://{Request.Host}";
        await authService.SignUpAsync(signUpRequestDto, hostUri);
        return Ok("SignUp Done Successfully!");
    }

    [HttpPost("login")]
    [HandleRequestResponse(TypeResponse = Domain.Enums.Enums.ETypeRequestResponse.ResponseWithData)]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
    {
        string token = await authService.LoginAsync(loginRequestDto);
        return Ok(token);
    }

    [HttpGet("authtest")]
    [HandleRequestResponse]
    [Authorize]
    public IActionResult AuthorizeTest()
    {
        return Ok("Test");
    }
    [HttpGet("magiclogin")]
    [HandleRequestResponse]
    public async Task<IActionResult> MagicLogin([FromQuery] string token)
    {
        await authService.MagicLoginAsync(token);
        return Ok("User Verified!");
    }

}
