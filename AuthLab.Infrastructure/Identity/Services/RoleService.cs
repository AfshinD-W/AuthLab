using AuthLab.Application.DTO.Role;
using AuthLab.Application.Exceptions;
using AuthLab.Application.Interfaces;
using AuthLab.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;

namespace AuthLab.Infrastructure.Identity.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<Role> _roleManager;

        public RoleService(RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<RoleResponseDTO> CreateRoleAsync(RoleRequestDTO requestDTO)
        {
            Role role = new()
            {
                Name = requestDTO.Name,
            };

            IdentityResult result = await _roleManager.CreateAsync(role);

            if (!result.Succeeded)
            {
                throw new ValidationException(result.Errors.Select(e => e.Description));
            }

            return new RoleResponseDTO { Name = role.Name };
        }

        public async Task DeleteRoleAsync(string id)
        {
            Role? role = await _roleManager.FindByIdAsync(id) ?? throw new NotFoundException("Role not found");

            IdentityResult result = await _roleManager.DeleteAsync(role);

            if (!result.Succeeded)
                throw new BusinessException(result.Errors.Select(e => e.Description));
        }
    }
}
