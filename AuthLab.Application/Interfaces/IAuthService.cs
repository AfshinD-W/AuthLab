using AuthLab.Application.DTO.Login;
using AuthLab.Application.DTO.Password;

namespace AuthLab.Application.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponseDto> LoginAsync(LoginRequestDto request);
        Task<LoginResponseDto> RefreshTokenAsync(string refreshToken);
        Task LogOutAsync(string refreshToken);
        Task ForgotPasswordAsync(ForgotPasswordRequestDto request);
        Task ResetPasswordAsync(ResetPasswordRequestDto request);
    }
}
