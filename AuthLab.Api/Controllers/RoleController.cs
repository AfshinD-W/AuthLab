using AuthLab.Api.Response;
using AuthLab.Application.DTO.Role;
using AuthLab.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AuthLab.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet("get-all-roles")]
        public async Task<IActionResult> GetRolesAsync()
        {
            var result = await _roleService.GetRolesAsync();

            ApiResponse<List<RoleResponseDto>> response = new()
            {
                Success = true,
                Data = result
            };

            return Ok(result);
        }

        [HttpPost("create-role")]
        public async Task<IActionResult> CreateRoleAsync(RoleRequestDto requestDto)
        {
            var result = await _roleService.CreateRoleAsync(requestDto);

            ApiResponse<RoleResponseDto> response = new()
            {
                Success = true,
                Message = "Role created successfully",
                Data = result
            };

            return Ok(response);
        }

        [HttpDelete("delete-role")]
        public async Task<IActionResult> DeleteRoleAsync(string id)
        {
            await _roleService.DeleteRoleAsync(id);

            ApiResponse<RoleResponseDto> response = new()
            {
                Success = true,
                Message = "Role deleted successfully",
            };

            return Ok(response);
        }

    }
}
