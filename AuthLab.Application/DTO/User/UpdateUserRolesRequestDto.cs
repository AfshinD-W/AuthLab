namespace AuthLab.Application.DTO.User
{
    public class UpdateUserRolesRequestDto
    {
        public required string UserId { get; set; }
        public ICollection<string>? RoleNames { get; set; }
    }
}
