using AuthLab.Api.Response;
using AuthLab.Application.DTO.User;
using AuthLab.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthLab.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUserRoleService _userRoleService;
        public UserController(IUserService userService, IUserRoleService userRoleService)
        {
            _userService = userService;
            _userRoleService = userRoleService;
        }

        [HttpGet("get-all-users")]
        public async Task<IActionResult> GetUsersAsync()
        {
            var result = await _userService.GetUsersAsync();

            ApiResponse<List<UserResponseDto>> response = new()
            {
                Success = true,
                Data = result
            };

            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("create-user")]
        public async Task<IActionResult> CreateUserAsync(CreateUserRequestDto dto)
        {
            var result = await _userService.CreateUserAsync(dto);

            ApiResponse<UserResponseDto> response = new()
            {
                Success = true,
                Message = "User created successfully",
                Data = result
            };

            return Ok(response);
        }

        [HttpPut("update-User")]
        public async Task<IActionResult> UpdateUserAsync(UpdateUserRequestDto dto)
        {
            var result = await _userService.UpdateUserAsync(dto);

            ApiResponse<UserResponseDto> response = new()
            {
                Success = true,
                Message = "User updated successfully",
                Data = result
            };

            return Ok(response);
        }

        [HttpDelete("delete-user")]
        public async Task<IActionResult> DeleteUserAsync(string id)
        {
            await _userService.DeleteUserAsync(id);

            ApiResponse<UserResponseDto> response = new()
            {
                Success = true,
                Message = "User deleted successfully."
            };

            return Ok(response);
        }

        [HttpPost("sync-roles")]
        public async Task<IActionResult> SyncUserRolesAsync(UpdateUserRolesRequestDto dto)
        {
            var result = await _userRoleService.SyncUserRolesAsync(dto);

            ApiResponse<UpdateUserRolesRequestDto> response = new()
            {
                Success = true,
                Message = "Roles synced successfully.",
                Data = result,
            };

            return Ok(response);
        }
    }
}
