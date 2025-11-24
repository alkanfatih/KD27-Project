namespace Application.DTOs.UserDTOs
{
    public record UserListDto
    {
        public int Id { get; init; }
        public string UserName { get; init; }
        public string Email { get; init; }
        public string PhoneNumber { get; init; }
        public string Role { get; init; }
    }
}
