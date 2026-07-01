using AuthLab.Application.DTO.Jwt;

namespace AuthLab.Application.Interfaces
{
    public interface IJwtService
    {
        JwtResponse GenerateAccessToken(JwtUserInfoDto userInfo);
    }
}
