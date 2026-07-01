using AuthLab.Application.DTO.Jwt;
using AuthLab.Application.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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
            List<Claim> claims =
                [
                    new(JwtRegisteredClaimNames.Sub, userInfo.UserId),
                    new(JwtRegisteredClaimNames.Email, userInfo.Email),
                    new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                ];

            claims.AddRange(userInfo.Roles.Select(role => new Claim(ClaimTypes.Role, role)));

            string secretKey = _jwtOptions.SecretKey;

            SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(secretKey));

            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.Sha256);

            JwtSecurityToken token = new(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtOptions.ExpireMinutes),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
