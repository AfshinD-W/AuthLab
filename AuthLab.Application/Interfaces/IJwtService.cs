using AuthLab.Application.DTO.Jwt;

namespace AuthLab.Application.Interfaces
{
    public interface IJwtService
    {
        string GenerateAccessToken(JwtUserInfoDto userInfo);
    }
}
