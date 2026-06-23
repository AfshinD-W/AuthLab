using AuthLab.Application.DTO;

namespace AuthLab.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserResponseDTO> CreateUserAsync(UserRequestDTO requestDTO);
    }
}
