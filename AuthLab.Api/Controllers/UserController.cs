using AuthLab.Application.DTO;
using AuthLab.Infrastructure.Identity.Services;
using Microsoft.AspNetCore.Mvc;

namespace AuthLab.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("create-user")]
        public async Task<IActionResult> CreateUserAsync(UserRequestDTO requestDTO)
        {
            var result = await _userService.CreateUserAsync(requestDTO);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}
