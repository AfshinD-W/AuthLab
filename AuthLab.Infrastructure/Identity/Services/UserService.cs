using AuthLab.Application.DTO.User;
using AuthLab.Application.Exceptions;
using AuthLab.Application.Interfaces;
using AuthLab.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;

namespace AuthLab.Infrastructure.Identity.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;

        public UserService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<UserResponseDTO> CreateUserAsync(UserRequestDTO requestDTO)
        {
            User user = new()
            {
                UserName = requestDTO.UserName,
                Email = requestDTO.Email,
                PhoneNumber = requestDTO.PhoneNumber,
            };

            IdentityResult result = await _userManager.CreateAsync(user, requestDTO.Password);

            if (!result.Succeeded)
            {
                throw new ValidationException(result.Errors.Select(x => x.Description));
            }

            return new UserResponseDTO()
            {
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
            };
        }
    }
}
