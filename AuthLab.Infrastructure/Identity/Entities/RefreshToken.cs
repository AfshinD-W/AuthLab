namespace AuthLab.Infrastructure.Identity.Entities
{
    public class RefreshToken
    {
        public Guid Id { get; set; }

        public string Token { get; set; } = string.Empty;

        public DateTime ExpiresAt { get; set; }

        public DateTime? RevokedAt { get; set; }

        public string UserId { get; set; } = string.Empty;

        public User User { get; set; } = default!;
    }
}
