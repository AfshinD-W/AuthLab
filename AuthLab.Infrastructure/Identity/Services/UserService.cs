using AuthLab.Application.DTO.User;
using AuthLab.Application.Exceptions;
using AuthLab.Application.Interfaces;
using AuthLab.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;

namespace AuthLab.Infrastructure.Identity.Services
{
    public class UserService : IUserService
    {
        private const string UserNotFound = "User not found.";

        private readonly UserManager<User> _userManager;

        public UserService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<UserResponseDTO> CreateUserAsync(CreateUserRequestDTO requestDTO)
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

        public async Task<UserResponseDTO> UpdateUserAsync(UpdateUserRequestDTO requestDTO)
        {
            User? user = await _userManager.FindByIdAsync(requestDTO.Id) ?? throw new NotFoundException(UserNotFound);

            user.UserName = requestDTO.UserName;
            user.Email = requestDTO.Email;
            user.PhoneNumber = requestDTO.PhoneNumber;

            IdentityResult result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
                throw new ValidationException(result.Errors.Select(e => e.Description));

            return new UserResponseDTO()
            {
                Id = user.Id,
                UserName = requestDTO.UserName,
                Email = requestDTO.Email,
                PhoneNumber = requestDTO.PhoneNumber,
            };
        }

        public async Task DeleteUserAsync(string id)
        {
            User user = await _userManager.FindByIdAsync(id) ?? throw new NotFoundException(UserNotFound);

            IdentityResult result = await _userManager.DeleteAsync(user);

            if (!result.Succeeded)
                throw new BusinessException(result.Errors.Select(e => e.Description));
        }
    }
}
