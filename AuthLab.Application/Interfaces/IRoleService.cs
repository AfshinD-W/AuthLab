using AuthLab.Application.DTO.Role;

namespace AuthLab.Application.Interfaces
{
    public interface IRoleService
    {
        Task<List<string>> GetStringsAsync();
        Task<RoleResponseDto> CreateRoleAsync(RoleRequestDto requestDTO);
        Task DeleteRoleAsync(string id);
    }
}
