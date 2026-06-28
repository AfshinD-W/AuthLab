using AuthLab.Api.Response;
using AuthLab.Application.DTO.User;
using AuthLab.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AuthLab.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("create-user")]
        public async Task<IActionResult> CreateUserAsync(CreateUserRequestDTO requestDTO)
        {
            var result = await _userService.CreateUserAsync(requestDTO);

            ApiResponse<UserResponseDTO> response = new()
            {
                Success = true,
                Message = "User created successfully",
                Data = result
            };

            return Ok(response);
        }

        [HttpPut("update-User")]
        public async Task<IActionResult> UpdateUserAsync(UpdateUserRequestDTO requestDTO)
        {
            var result = await _userService.UpdateUserAsync(requestDTO);

            ApiResponse<UserResponseDTO> response = new()
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

            ApiResponse<UserResponseDTO> response = new()
            {
                Success = true,
                Message = "User deleted successfully."
            };

            return Ok(response);
        }
    }
}
