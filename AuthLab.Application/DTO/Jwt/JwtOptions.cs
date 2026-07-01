namespace AuthLab.Application.DTO.Jwt
{
    public class JwtOptions
    {
        public required string SecretKey { get; init; }
        public required string Issuer { get; init; }
        public required string Audience { get; init; }
        public int ExpireMinutes { get; init; }
    }
}
