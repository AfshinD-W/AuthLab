using AuthLab.Application.DTO.Jwt;
using AuthLab.Application.Interfaces;
using Microsoft.Extensions.Options;

namespace AuthLab.Infrastructure.Identity.Services
{
    public class JwtService : IJwtService
    {
        private readonly JwtOptions _jwtOptions;
        public JwtService(IOptions<JwtOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
        }

        public string GenerateAccessToken(JwtUserInfoDto userInfo)
        {
            throw new NotImplementedException();
        }
    }
}
