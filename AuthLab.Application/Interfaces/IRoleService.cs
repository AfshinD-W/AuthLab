using AuthLab.Application.DTO.Role;

namespace AuthLab.Application.Interfaces
{
    public interface IRoleService
    {
        Task<List<RoleResponseDto>> GetRolesAsync();
        Task<RoleResponseDto> CreateRoleAsync(RoleRequestDto requestDTO);
        Task DeleteRoleAsync(string id);
    }
}
