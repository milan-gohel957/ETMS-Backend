using ETMS.Service.DTOs;
using ETMS.Domain.Entities;
using ETMS.Repository.Helpers;
using ETMS.Repository.Repositories.Interfaces;
using ETMS.Service.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using static ETMS.Domain.Enums.Enums;
using ETMS.Service.Exceptions;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using AutoMapper;

namespace ETMS.Service.Services;

public class AuthService(IUnitOfWork unitOfWork, IHostEnvironment environment, IConfiguration configuration, ITokenService tokenService, IMapper mapper) : IAuthService
{
    private IGenericRepository<User>? _userRepo;
    private IGenericRepository<User> UserRepo => _userRepo ??= unitOfWork.GetRepository<User>();

    private IGenericRepository<Role>? _roleRepo;
    private IGenericRepository<Role> RoleRepo => _roleRepo ??= unitOfWork.GetRepository<Role>();

    private IGenericRepository<UserRole>? _userRoleRepo;
    private IGenericRepository<UserRole> UserRoleRepo => _userRoleRepo ??= unitOfWork.GetRepository<UserRole>();

    private IGenericRepository<UserRefreshToken> UserRefreshTokenRepo = unitOfWork.GetRepository<UserRefreshToken>();
    public async Task SignUpAsync(SignUpRequestDto signUpRequestDto, string hostUri)
    {

        //Check if User already exists with this email or username

        User? dbUser = await UserRepo.FirstOrDefaultAsync(x => !x.IsDeleted && x.Email.ToLower() == signUpRequestDto.Email.ToLower() || x.UserName.Equals(signUpRequestDto.Username));

        string randomToken = GenerateRandomToken.GenerateToken();

        if (dbUser != null)
        {
            //First check if user is verified
            if (dbUser.IsVerifiedUser)
                throw new ResponseException(EResponse.BadRequest, "User With Same Email Or Username already exists.");

            //if not verified and link expired send new one
            if (dbUser.MagicLinkTokenExpiry <= DateTime.UtcNow)
            {
                dbUser.MagicLinkToken = Guid.NewGuid().ToString();
                dbUser.MagicLinkTokenExpiry = DateTime.UtcNow.AddHours(24);
                await unitOfWork.SaveChangesAsync(); // Save the new token!
                await SendMagicLinkAsync(hostUri, dbUser.MagicLinkToken, signUpRequestDto.Email);
                return;
            }
            throw new ResponseException(EResponse.BadRequest, "Verification Pending. Please check your email.");
        }


        User newUser = new()
        {
            FirstName = signUpRequestDto.Firstname,
            LastName = signUpRequestDto.Lastname,
            UserName = signUpRequestDto.Username,
            Email = signUpRequestDto.Email,
            MagicLinkToken = randomToken,
            MagicLinkTokenExpiry = DateTime.UtcNow.AddHours(24),
            PasswordHash = HashHelper.HashPassword(signUpRequestDto.Password),
            CreatedAt = DateTime.UtcNow,
        };

        User addedUser = await UserRepo.AddAsync(newUser);
        await unitOfWork.SaveChangesAsync();

        UserRole userRole = new()
        {
            UserId = addedUser.Id,
            RoleId = (int)RoleEnum.Admin,
            IsDeleted = false,
            CreatedAt = DateTime.UtcNow
        };

        var userRoleRepo = unitOfWork.GetRepository<UserRole>();

        await userRoleRepo.AddAsync(userRole);
        await unitOfWork.SaveChangesAsync();

        await SendMagicLinkAsync(hostUri, randomToken, signUpRequestDto.Email);
    }

    private async Task SendMagicLinkAsync(string hostUri, string randomToken, string toEmail)
    {
        var templatePath = Path.Combine(
                environment.ContentRootPath,
                "..",
                "ETMS.Repository",
                "EmailTemplates",
                "WelcomeLoginLink.html");
        // var templatePath = Path.Combine(basePath, "ETMS.Infrastructure", "EmailTemplates", "WelcomeLoginLink.html");
        var htmlTemplate = await File.ReadAllTextAsync(templatePath);
        var SignInLink = $"{hostUri}/magiclogin?token={randomToken}";

        htmlTemplate = htmlTemplate.Replace("{{SignInLink}}", SignInLink);

        var server = configuration["EmailConfiguration:SmtpServer"] ?? string.Empty;
        var port = configuration["EmailConfiguration:Port"] ?? string.Empty;
        var userName = configuration["EmailConfiguration:Username"] ?? string.Empty;
        var password = configuration["EmailConfiguration:AppPassword"] ?? string.Empty;
        var fromEmail = configuration["EmailConfiguration:From"] ?? string.Empty;

        await EmailHelper.SendHtmlEmailAsync(toEmail, "Welcome to Taskss! Sign in to your account", htmlTemplate, userName, server, password, Convert.ToInt32(port), fromEmail);
    }

    public async Task MagicLoginAsync(string token)
    {
        User? dbUser = await UserRepo.FirstOrDefaultAsync(u => u.MagicLinkToken == token) ?? throw new ResponseException(EResponse.NotFound, "Invalid Link.");

        if (dbUser.MagicLinkTokenExpiry <= DateTime.UtcNow) throw new ResponseException(EResponse.BadRequest, "Token Link Expired");

        dbUser.MagicLinkToken = null;
        dbUser.MagicLinkTokenExpiry = null;
        dbUser.IsVerifiedUser = true;
        await unitOfWork.SaveChangesAsync();
    }

    public async Task<LoginResponseDto> LoginAsync(LoginRequestDto loginRequestDto)
    {
        string email = loginRequestDto.Email;
        string password = loginRequestDto.Password;

        var user = await UserRepo.FirstOrDefaultAsync(u =>
            u.Email.ToLower() == email.ToLower() && !u.IsDeleted);

        if (user == null)
            throw new ResponseException(EResponse.NotFound, "User not found.");

        if (!user.IsVerifiedUser)
            throw new ResponseException(EResponse.BadRequest, "User has not verified their email.");

        if (!HashHelper.VerifyPassword(user.PasswordHash, password))
            throw new ResponseException(EResponse.BadRequest, "Invalid credentials.");

        var (accessToken, accessExpiresAt) = tokenService.GenerateAccessToken(user);
        var (refreshToken, refreshExpiresAt, guid) = tokenService.GenerateRefreshToken();

        await UserRefreshTokenRepo.AddAsync(new UserRefreshToken()
        {
            ExpiresAt = refreshExpiresAt,
            Guid = guid,
            CreatedAt = DateTime.UtcNow,
            IpAddress = loginRequestDto.UserHostAddress,
            IsBlocked = false,
            IsExpired = false,
            UserId = user.Id
        });

        await unitOfWork.SaveChangesAsync();
        return new LoginResponseDto()
        {
            AccessToken = accessToken,
            AccessExpiresAt = accessExpiresAt,
            RefreshToken = refreshToken,
            RefreshTokenExpiresAt = refreshExpiresAt,
        };
    }

    public async Task<CurrentUserDto> GetCurrentUserDtoAsync(int userId)
    {
        User? dbUser = await UserRepo.GetByIdAsync(userId);

        if (dbUser == null) throw new ResponseException(EResponse.BadRequest, "Invalid Token.");

        return mapper.Map<CurrentUserDto>(dbUser);
    }

    public async Task<LoginResponseDto> RefreshTokenAsync(string refreshToken, string? ipAddress)
    {
        ClaimsPrincipal claimsPrincipal = tokenService.ValidateAndDecodeToken(refreshToken);

        string? guid = claimsPrincipal.FindFirst(JwtRegisteredClaimNames.Jti)?.Value.ToString();

        if (string.IsNullOrEmpty(guid))
            throw new ResponseException(EResponse.Unauthorized, "Invalid Token.");

        UserRefreshToken? userRefreshToken = await UserRefreshTokenRepo.FirstOrDefaultAsync(x => x.Guid == guid);

        if (userRefreshToken == null)
            throw new ResponseException(EResponse.Unauthorized, "Invalid Token.");

        User? dbUser = await UserRepo.FirstOrDefaultAsync(x => x.Id == userRefreshToken.UserId);
        if (dbUser == null)
            throw new ResponseException(EResponse.Unauthorized, "Invalid Token.");

        var (newAccessToken, newAccessExpiresAt) = tokenService.GenerateAccessToken(dbUser);
        var (newRefreshToken, newRefreshExpiresAt, newGuid) = tokenService.GenerateRefreshToken();

        UserRefreshTokenRepo.Update(new()
        {
            ExpiresAt = newRefreshExpiresAt,
            Guid = newGuid,
            CreatedAt = DateTime.UtcNow,
            IpAddress = ipAddress,
            IsBlocked = false,
            IsExpired = false,
            UserId = dbUser.Id
        });

        await unitOfWork.SaveChangesAsync();

        return new()
        {
            AccessExpiresAt = newAccessExpiresAt,
            RefreshToken = newRefreshToken,
            RefreshTokenExpiresAt = newRefreshExpiresAt,
            AccessToken = newAccessToken
        };
    }

}
