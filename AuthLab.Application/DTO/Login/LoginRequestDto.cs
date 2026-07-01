namespace AuthLab.Application.DTO.Login
{
    public class LoginRequestDto
    {
        public required string UserName { get; init; }
        public required string Password { get; init; }
    }
}
