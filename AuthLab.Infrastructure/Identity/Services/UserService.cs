using AuthLab.Application.DTO.User;
using AuthLab.Application.Exceptions;
using AuthLab.Application.Interfaces;
using AuthLab.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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

        public async Task<List<UserResponseDto>> GetUsersAsync()
        {
            var existsUsers = await _userManager.Users.ToListAsync() ?? throw new NotFoundException("There is no user in database.");

            List<UserResponseDto> response = [.. existsUsers.Select(u => new UserResponseDto
            {
                Id = u.Id,
                UserName = u.UserName ?? string.Empty,
                Email = u.Email ?? string.Empty,
                PhoneNumber = u.PhoneNumber,
            })];

            return response;
        }

        public async Task<UserResponseDto> CreateUserAsync(CreateUserRequestDto requestDto)
        {
            User user = new()
            {
                UserName = requestDto.UserName,
                Email = requestDto.Email,
                PhoneNumber = requestDto.PhoneNumber,
            };

            IdentityResult result = await _userManager.CreateAsync(user, requestDto.Password);

            if (!result.Succeeded)
            {
                throw new ValidationException(result.Errors.Select(x => x.Description));
            }

            return new UserResponseDto()
            {
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
            };
        }

        public async Task<UserResponseDto> UpdateUserAsync(UpdateUserRequestDto requestDto)
        {
            User? user = await _userManager.FindByIdAsync(requestDto.Id) ?? throw new NotFoundException(UserNotFound);

            user.UserName = requestDto.UserName;
            user.Email = requestDto.Email;
            user.PhoneNumber = requestDto.PhoneNumber;

            IdentityResult result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
                throw new ValidationException(result.Errors.Select(e => e.Description));

            return new UserResponseDto()
            {
                Id = user.Id,
                UserName = requestDto.UserName,
                Email = requestDto.Email,
                PhoneNumber = requestDto.PhoneNumber,
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
