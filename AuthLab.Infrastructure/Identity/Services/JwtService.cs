using AuthLab.Application.DTO.Jwt;
using AuthLab.Application.Interfaces;

namespace AuthLab.Infrastructure.Identity.Services
{
    public class JwtService : IJwtService
    {
        public string GenerateAccessToken(JwtUserInfoDto userInfo)
        {
            throw new NotImplementedException();
        }
    }
}
