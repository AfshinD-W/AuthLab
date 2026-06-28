using AuthLab.Application.DTO.User;

namespace AuthLab.Application.Interfaces
{
    public interface IUserService
    {
        Task<List<UserResponseDto>> GetUsersAsync();
        Task<UserResponseDto> CreateUserAsync(CreateUserRequestDto requestDto);
        Task<UserResponseDto> UpdateUserAsync(UpdateUserRequestDto requestDto);
        Task DeleteUserAsync(string id);
    }
}
