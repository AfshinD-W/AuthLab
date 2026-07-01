using AuthLab.Application.DTO.Jwt;
using AuthLab.Application.DTO.Login;
using AuthLab.Application.Exceptions;
using AuthLab.Application.Interfaces;
using AuthLab.Infrastructure.Database;
using AuthLab.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;

namespace AuthLab.Infrastructure.Identity.Services
{
    public class AuthService : IAuthService
    {
        private const string InvalidRefreshToken = "Invalid refresh token.";

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IJwtService _jwtService;
        private readonly RefreshTokenOptions _refreshTokenOptions;
        private readonly ILogger<AuthService> _logger;
        private readonly AppDbContext _appDbContext;

        public AuthService(UserManager<User> userManager, SignInManager<User> signInManager, IJwtService jwtService, AppDbContext appDbContext, IOptions<RefreshTokenOptions> refreshTokenOptions, ILogger logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtService = jwtService;
            _refreshTokenOptions = refreshTokenOptions.Value;
            _logger = logger;
            _appDbContext = appDbContext;
        }

        public async Task<LoginResponseDto> LoginAsync(LoginRequestDto request)
        {
            User? user = await _userManager.FindByEmailAsync(request.Email)
                ?? throw new UnauthorizedException("Invalid email or password.");

            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, lockoutOnFailure: true);

            if (!result.Succeeded)
                throw new UnauthorizedException("Invalid email or password.");

            IList<string> roles = await _userManager.GetRolesAsync(user);

            JwtUserInfoDto userInfo = new()
            {
                UserId = user.Id,
                Email = request.Email,
                Roles = roles
            };

            var jwtResponse = _jwtService.GenerateAccessToken(userInfo);

            RefreshToken refreshToken = CreateRefreshToken(user.Id);

            await _appDbContext.RefreshTokens.AddAsync(refreshToken);
            await _appDbContext.SaveChangesAsync();

            return new LoginResponseDto()
            {
                AccessToken = jwtResponse.Token,
                RefreshToken = refreshToken.Token,
                ExpiresAt = jwtResponse.ExpiresAt,
            };
        }

        public async Task<LoginResponseDto> RefreshTokenAsync(string refreshToken)
        {
            RefreshToken refresh = await _appDbContext.RefreshTokens.Include(r => r.User).SingleOrDefaultAsync(r => r.Token == refreshToken) ?? throw new UnauthorizedException(InvalidRefreshToken);

            if (refresh.IsRevoked)
            {
                if (!string.IsNullOrWhiteSpace(refresh.ReplacedByToken))
                {
                    var activeRefreshTokens = await _appDbContext.RefreshTokens
                        .Where(t => t.UserId == refresh.UserId && !t.IsRevoked)
                        .ToListAsync();

                    foreach (var token in activeRefreshTokens)
                    {
                        token.RevokedAt = DateTime.UtcNow;
                    }

                    _logger.LogWarning("Refresh token reuse detected for user {UserId}", refresh.UserId);

                    await _appDbContext.SaveChangesAsync();
                }

                throw new UnauthorizedException(InvalidRefreshToken);
            }

            if (refresh.IsExpired)
                throw new UnauthorizedException(InvalidRefreshToken);

            var userRoles = await _userManager.GetRolesAsync(refresh.User);

            JwtUserInfoDto userInfo = new()
            {
                UserId = refresh.User.Id,
                Email = refresh.User.Email!,
                Roles = userRoles,
            };

            var jwtResponse = _jwtService.GenerateAccessToken(userInfo);

            RefreshToken newRefreshToken = CreateRefreshToken(refresh.UserId);

            refresh.RevokedAt = DateTime.UtcNow;
            refresh.ReplacedByToken = newRefreshToken.Token;

            await _appDbContext.RefreshTokens.AddAsync(newRefreshToken);
            await _appDbContext.SaveChangesAsync();

            return new LoginResponseDto()
            {
                AccessToken = jwtResponse.Token,
                RefreshToken = newRefreshToken.Token,
                ExpiresAt = jwtResponse.ExpiresAt,
            };
        }

        private RefreshToken CreateRefreshToken(string userId)
        {
            return new()
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddDays(_refreshTokenOptions.ExpireDays),
                UserId = userId
            };
        }
    }
}
