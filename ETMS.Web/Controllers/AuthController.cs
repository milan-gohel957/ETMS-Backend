using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using AutoWrapper.Wrappers;
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
[Route("api/auth")]
public class AuthController(IAuthService authService, IHostEnvironment hostEnvironment) : BaseApiController
{
    [HttpPost("signup")]
    public async Task<IActionResult> SignUp([FromBody] SignUpRequestDto signUpRequestDto)
    {
        var hostUri = $"{Request.Scheme}://{Request.Host}";
        await authService.SignUpAsync(signUpRequestDto, hostUri);
        return Success("Sign-up successful. Check your email to verify your account.");
    }
    [HttpGet("me")]
    [Authorize]
    public async Task<IActionResult> GetCurrentUserProfile()
    {
        string? userIdString = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userIdString) || !int.TryParse(userIdString, out var userId))
        {
            throw new ResponseException(EResponse.Unauthorized, "Token Invalid (UserId not found or not an integer).");
        }

        var currentUser = await authService.GetCurrentUserDtoAsync(userId);

        return Success(currentUser);
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshToken(RefreshTokenRequestDto refreshTokenRequestDto)
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

        return Success(loginResponseDto, "Token Refreshed Successfully!");
    }
    [HttpPost("login/google")]
    public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginDto googleAuthDto)
    {
        var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
        googleAuthDto.IpAddress = ipAddress;
        var loginResponse = await authService.AuthenticateWithGoogleAsync(
            googleAuthDto
        );
       
        return Success(loginResponse, "Login Successfull!");
    }

    [HttpPost("login")]
    // [HandleRequestResponse(TypeResponse = ETypeRequestResponse.ResponseWithData)]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
    {
        loginRequestDto.UserHostAddress = GetUserIpAddress();

        LoginResponseDto loginResponseDto = await authService.LoginAsync(loginRequestDto);

        Response.Cookies.Append("AccessToken", loginResponseDto.AccessToken, new()
        {
            Expires = loginResponseDto.AccessExpiresAt,
            HttpOnly = !hostEnvironment.IsDevelopment(),
            Secure = !hostEnvironment.IsDevelopment(),
            SameSite = SameSiteMode.None,
        });

        Response.Cookies.Append("RefreshToken", loginResponseDto.RefreshToken, new()
        {
            Expires = loginResponseDto.RefreshTokenExpiresAt,
            HttpOnly = !hostEnvironment.IsDevelopment(),
            Secure = !hostEnvironment.IsDevelopment(),
            SameSite = SameSiteMode.None
        });

        return Success(loginResponseDto, "Login Successful!");
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
    [Authorize]
    public IActionResult AuthorizeTest()
    {
        return Success<object>(null, "Authorization Test Successful!");
    }

    [HttpPost("magiclogin")]
    public async Task<IActionResult> MagicLogin([FromQuery] string token)
    {
        await authService.MagicLoginAsync(token);
        return Success<object>(null, "User verified successfully!");
    }
}