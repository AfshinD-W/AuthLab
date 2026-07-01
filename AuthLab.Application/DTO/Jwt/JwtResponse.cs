namespace AuthLab.Application.DTO.Jwt
{
    public class JwtResponse
    {
        public required string Token { get; init; }
        public DateTime ExpiresAt { get; init; }
    }
}
