using Application.DTOs.CartItemDTOs;
using Domain.Entities;
using Domain.RequestParameters;

namespace Application.Interfaces
{
    public interface IShoppingCartService
    {
        Task<ShoppingCart> GetOrCreateCartAsync(int userId);
        Task AddItemAsync(int userId, int productId, int quantity = 1);
        Task RemoveItemAsync(int userId, int productId);
        Task UpdateQuantityAsync(int userId, int productId, int quantity);
        Task ClearCartAsync(int userId);
        void Update(ShoppingCart cart);
        Task<IEnumerable<CartItemDto>> GetCartItemDtoAsync(int userId);
        Task<IEnumerable<AdminCartDto>> GetAllCartsForAdminAsync();
        Task<IEnumerable<AdminCartDto>> GetFilteredCartsAsync(CartFilterParameters filters);


    }
}
