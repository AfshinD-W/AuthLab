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

            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}
