namespace Application.DTOs.UserDTOs
{
    public record CreateUserDto
    {
        public string FullName { get; init; }
        public string Email { get; init; }
        public string UserName { get; init; }
        public string Password { get; init; }
    }
}
