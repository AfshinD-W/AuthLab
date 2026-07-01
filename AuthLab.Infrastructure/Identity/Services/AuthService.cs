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

        public AuthService(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<LoginResponseDto> LoginAsync(LoginRequestDto request)
        {
            User? user = await _userManager.FindByEmailAsync(request.Email)
                ?? throw new UnauthorizedException("Invalid username or password.");

            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, true);

            if (!result.Succeeded)
                throw new UnauthorizedException("Invalid username or password.");

            IList<string> roles = await _userManager.GetRolesAsync(user);
        }
    }
}
