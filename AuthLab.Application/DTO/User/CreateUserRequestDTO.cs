namespace AuthLab.Application.DTO.User
{
    public class CreateUserRequestDto
    {
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
