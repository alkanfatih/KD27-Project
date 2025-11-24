using Application.DTOs.UserDTOs;

namespace Application.Interfaces
{
    public interface ICustomerService
    {
        Task<int> RegisterAsync(CreateUserDto dto);
        Task<UserDto?> LoginAsync(LoginDto dto);
        Task UpdateAsync(UpdateUserDto dto);
        Task DeleteAsync(int id);
        Task<UserDto?> GetByIdAsync(int id);
        Task<IEnumerable<UserDto>> GetAllAsync();
        Task<List<UserListDto>> GetAllUsersAsync();
        Task<List<UserListDto>> GetUsersByRolesAsync(List<string> roles);
    }
}
