using AuthLab.Application.DTO.Login;
using AuthLab.Application.Interfaces;
using AuthLab.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;

namespace AuthLab.Infrastructure.Identity.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInResult;

        public AuthService(UserManager<User> userManager, SignInManager<User> signInResult)
        {
            _userManager = userManager;
            _signInResult = signInResult;
        }

        public Task<LoginResponseDto> LoginAsync(LoginRequestDto request)
        {
            throw new NotImplementedException();
        }
    }
}
