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

        [HttpPost("create-role")]
        public async Task<IActionResult> CreateRoleAsync(RoleRequestDTO requestDTO)
        {
            var result = await _roleService.CreateRoleAsync(requestDTO);

            ApiResponse<RoleResponseDTO> response = new()
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

            ApiResponse<RoleResponseDTO> response = new()
            {
                Success = true,
                Message = "Role deleted successfully",
            };

            return Ok(response);
        }

    }
}
