using AuthLab.Api.Response;
using AuthLab.Application.DTO.Login;
using AuthLab.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AuthLab.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(LoginRequestDto dto)
        {
            var result = await _authService.LoginAsync(dto);

            ApiResponse<LoginResponseDto> response = new()
            {
                Success = true,
                Message = "User login was successfully",
                Data = result,
            };

            return Ok(response);
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshTokenAsync(string refreshToken)
        {
            var result = await _authService.RefreshTokenAsync(refreshToken);

            ApiResponse<LoginResponseDto> response = new()
            {
                Success = true,
                Message = "User login was successfully",
                Data = result,
            };

            return Ok(response);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> LogOut(string refreshToken)
        {
            await _authService.LogOutAsync(refreshToken);

            var response = new ApiResponse<object>()
            {
                Success = true,
                Message = "You logout successfully",
            };

            return Ok();
        }
    }
}
