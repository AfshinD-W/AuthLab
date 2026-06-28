using AuthLab.Application.DTO.User;

namespace AuthLab.Application.Interfaces
{
    public interface IUserRoleService
    {
        Task<UpdateUserRolesRequestDto> SyncUserRolesAsync(UpdateUserRolesRequestDto requestDTO);
    }
}
