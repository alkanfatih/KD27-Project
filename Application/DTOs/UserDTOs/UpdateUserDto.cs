namespace Application.DTOs.UserDTOs
{
    public record UpdateUserDto : BaseDto
    {
        public string FullName { get; init; }
        public string Email { get; init; }
        public string UserName { get; init; }
    }
}
