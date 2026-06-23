using AuthLab.Application.DTO;
using AuthLab.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;

namespace AuthLab.Infrastructure.Identity.Services
{
    public class UserService
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
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception(errors);
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
