namespace AuthLab.Application.DTO.Jwt
{
    public class JwtUserInfoDto
    {
        public required string UserId { get; init; }
        public required string Email { get; init; }
        public required IList<string> Roles { get; init; }
    }
}
