using AuthLab.Application.DTO.User;

namespace AuthLab.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserResponseDTO> CreateUserAsync(UserRequestDTO requestDTO);
    }
}
