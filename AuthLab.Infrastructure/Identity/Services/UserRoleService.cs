using AuthLab.Application.DTO.User;
using AuthLab.Application.Exceptions;
using AuthLab.Application.Interfaces;
using AuthLab.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;

namespace AuthLab.Infrastructure.Identity.Services
{
    public class UserRoleService : IUserRoleService
    {
        private readonly UserManager<User> _userManager;
        public UserRoleService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<UpdateUserRolesRequestDto> SyncUserRolesAsync(UpdateUserRolesRequestDto requestDTO)
        {
            User user = await _userManager.FindByIdAsync(requestDTO.UserId) ?? throw new NotFoundException("User not found.");
            var userRoles = await _userManager.GetRolesAsync(user);

            var newRoles = requestDTO.RoleNames?.Except(userRoles).ToList() ?? [];
            var deletedRoles = userRoles.Except(requestDTO.RoleNames ?? []).ToList();

            if (newRoles.Count > 0)
            {
                IdentityResult addedRoles = await _userManager.AddToRolesAsync(user, newRoles);

                if (!addedRoles.Succeeded)
                    throw new BusinessException(addedRoles.Errors.Select(e => e.Description));
            }

            if (deletedRoles.Count > 0)
            {
                IdentityResult removedRoles = await _userManager.RemoveFromRolesAsync(user, deletedRoles);

                if (!removedRoles.Succeeded)
                    throw new BusinessException(removedRoles.Errors.Select(e => e.Description));
            }

            return new() { UserId = user.Id, RoleNames = requestDTO.RoleNames };
        }
    }
}
