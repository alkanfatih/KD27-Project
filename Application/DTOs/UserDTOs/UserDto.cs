namespace Application.DTOs.UserDTOs
{
    public record UserDto : BaseDto
    {
        public string FullName { get; init; }
        public string Email { get; init; }
        public string UserName { get; init; }
    }
}
