namespace AuthLab.Application.DTO.User
{
    public class UserRequestDTO
    {
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
