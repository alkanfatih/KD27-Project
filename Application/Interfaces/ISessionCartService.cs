using Application.DTOs.CartItemDTOs;

namespace Application.Interfaces
{
    public interface ISessionCartService
    {
        List<CartItemDto> GetCart(); // Sadece DTO, çünkü Entity yok
        void AddToCart(CartItemDto item);
        void RemoveFromCart(int productId);
        void ClearCart();
        void UpdateQuantity(int productId, int quantity);
    }

}
