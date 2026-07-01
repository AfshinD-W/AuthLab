using AuthLab.Application.DTO.Jwt;
using AuthLab.Application.DTO.Login;
using AuthLab.Application.Exceptions;
using AuthLab.Application.Interfaces;
using AuthLab.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;

namespace AuthLab.Infrastructure.Identity.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IJwtService _jwtService;

        public AuthService(UserManager<User> userManager, SignInManager<User> signInManager, IJwtService jwtService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtService = jwtService;
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
                Email = user.Email ?? throw new UnauthorizedException("User email is null"),
                Roles = roles
            };

            var jwtResponse = _jwtService.GenerateAccessToken(userInfo);

            return new LoginResponseDto()
            {
                AccessToken = jwtResponse.Token,
                ExpiresAt = jwtResponse.ExpiresAt,
            };
        }
    }
}
