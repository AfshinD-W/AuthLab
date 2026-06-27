using AuthLab.Application.DTO.Role;

namespace AuthLab.Application.Interfaces
{
    public interface IRoleService
    {
        Task<RoleResponseDTO> CreateRoleAsync(RoleRequestDTO requestDTO);
    }
}
