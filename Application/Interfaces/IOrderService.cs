using Application.DTOs.OrderDTOs;

namespace Application.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> GetAllAsync();
        Task<IEnumerable<OrderDto>> GetAllByCustomerIdAsync(int customerId);
        Task<OrderDto?> GetByIdAsync(int id);
        Task<int> CreateOrderAsync(CreateOrderDto dto);
        Task CancelOrderAsync(int id);
        Task UpdateStatusAsync(int id, int newStatus);
    }
}
