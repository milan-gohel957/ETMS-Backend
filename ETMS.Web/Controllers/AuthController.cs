using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using ETMS.Domain.Common;
using ETMS.Service.DTOs;
using ETMS.Service.Exceptions;
using ETMS.Service.Services.Interfaces;
using ETMS.Web.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static ETMS.Domain.Enums.Enums;

namespace ETMS.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthService authService, IHostEnvironment hostEnvironment) : ControllerBase
{
    [HttpPost("signup")]
    [HandleRequestResponse]
    public async Task<Response<object>> SignUp([FromBody] SignUpRequestDto signUpRequestDto)
    {
        var hostUri = $"{Request.Scheme}://{Request.Host}";
        await authService.SignUpAsync(signUpRequestDto, hostUri);

        return new Response<object>()
        {
            Data = null,
            Message = "Sign Up Successful!",
            Errors = [],
            Succeeded = true,
            StatusCode = HttpStatusCode.OK
        };
    }
    [HttpGet("me")]
    [HandleRequestResponse(TypeResponse = ETypeRequestResponse.ResponseWithData)]
    [Authorize]
    public async Task<Response<CurrentUserDto>> GetCurrentUserProfile()
    {
        string? userIdString = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userIdString) || !int.TryParse(userIdString, out var userId))
        {
            throw new ResponseException(EResponse.Unauthorized, "Token Invalid (UserId not found or not an integer).");
        }

        var currentUser = await authService.GetCurrentUserDtoAsync(userId);

        return new()
        {
            Data = currentUser,
            Errors = [],
            Succeeded = true,
            Message = "Success",
            StatusCode = HttpStatusCode.OK
        };
    }

    [HttpPost("refresh")]
    [HandleRequestResponse(TypeResponse = ETypeRequestResponse.ResponseWithData)]
    public async Task<Response<LoginResponseDto>> RefreshToken(RefreshTokenRequestDto refreshTokenRequestDto)
    {
        LoginResponseDto loginResponseDto = await authService.RefreshTokenAsync(refreshTokenRequestDto.RefreshToken, GetUserIpAddress());

        Response.Cookies.Append("AccessToken", loginResponseDto.AccessToken, new CookieOptions()
        {
            Expires = loginResponseDto.AccessExpiresAt,
            HttpOnly = true,
            Secure = !hostEnvironment.IsDevelopment(),
            SameSite = SameSiteMode.None,
        });

        Response.Cookies.Append("RefreshToken", loginResponseDto.RefreshToken, new CookieOptions()
        {
            Expires = loginResponseDto.RefreshTokenExpiresAt,
            HttpOnly = true,
            Secure = !hostEnvironment.IsDevelopment(),
            SameSite = SameSiteMode.None
        });

        return new Response<LoginResponseDto>()
        {
            Message = "Token Refreshed Successfully!",
            Data = loginResponseDto,
            StatusCode = HttpStatusCode.NoContent,
            Succeeded = true,
        };
    }

    [HttpPost("login")]
    [HandleRequestResponse(TypeResponse = ETypeRequestResponse.ResponseWithData)]
    public async Task<Response<LoginResponseDto>> Login([FromBody] LoginRequestDto loginRequestDto)
    {
        loginRequestDto.UserHostAddress = GetUserIpAddress();

        LoginResponseDto loginResponseDto = await authService.LoginAsync(loginRequestDto);

        Response.Cookies.Append("AccessToken", loginResponseDto.AccessToken, new()
        {
            Expires = loginResponseDto.AccessExpiresAt,
            HttpOnly = true,
            Secure = !hostEnvironment.IsDevelopment(),
            SameSite = SameSiteMode.None,
        });

        Response.Cookies.Append("RefreshToken", loginResponseDto.RefreshToken, new()
        {
            Expires = loginResponseDto.RefreshTokenExpiresAt,
            HttpOnly = true,
            Secure = !hostEnvironment.IsDevelopment(),
            SameSite = SameSiteMode.None
        });

        return new Response<LoginResponseDto>()
        {
            Data = loginResponseDto,
            Message = "Login Successful!",
            Errors = [],
            Succeeded = true,
            StatusCode = HttpStatusCode.OK
        };
    }

    private string? GetUserIpAddress()
    {
        string? ipAddress = string.Empty;

        // Check for proxy headers first
        if (HttpContext.Request.Headers.ContainsKey("X-Forwarded-For"))
        {
            ipAddress = HttpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault();
        }
        else if (HttpContext.Request.Headers.ContainsKey("X-Real-IP"))
        {
            ipAddress = HttpContext.Request.Headers["X-Real-IP"].FirstOrDefault();
        }
        else
        {
            ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
        }
        return ipAddress;
    }

    [HttpGet("authtest")]
    [HandleRequestResponse]
    [Authorize]
    public Response<object> AuthorizeTest()
    {
        return new Response<object>()
        {
            Data = null,
            Message = "Authorization Test Successful!",
            Errors = [],
            Succeeded = true,
            StatusCode = HttpStatusCode.NoContent
        };
    }

    [HttpPost("magiclogin")]
    [HandleRequestResponse]
    public async Task<Response<object>> MagicLogin([FromQuery] string token)
    {
        await authService.MagicLoginAsync(token);
        return new Response<object>()
        {
            Data = null,
            Message = "User Verified Successfully!",
            Errors = [],
            Succeeded = true,
            StatusCode = HttpStatusCode.NoContent
        };
    }
}