using Application.DTOs.CartItemDTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Application.Services
{
    public class SessionCartService : ISessionCartService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private const string SessionKey = "CART";

        public SessionCartService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public List<CartItemDto> GetCart()
        {
            var session = _contextAccessor.HttpContext!.Session;
            var json = session.GetString(SessionKey);
            return string.IsNullOrEmpty(json)
                ? new List<CartItemDto>()
                : JsonSerializer.Deserialize<List<CartItemDto>>(json)!;
        }

        public void SaveCart(List<CartItemDto> cart)
        {
            var json = JsonSerializer.Serialize(cart);
            _contextAccessor.HttpContext!.Session.SetString(SessionKey, json);
        }

        public void AddToCart(CartItemDto item)
        {
            var cart = GetCart();
            var existing = cart.FirstOrDefault(c => c.ProductId == item.ProductId);
            if (existing is not null)
                existing.Quantity += item.Quantity;
            else
                cart.Add(item);

            SaveCart(cart);
        }

        public void RemoveFromCart(int productId)
        {
            var cart = GetCart();
            var item = cart.FirstOrDefault(c => c.ProductId == productId);
            if (item != null)
            {
                cart.Remove(item);
                SaveCart(cart);
            }
        }

        public void ClearCart()
        {
            SaveCart(new List<CartItemDto>());
        }

        public void UpdateQuantity(int productId, int quantity)
        {
            var cart = GetCart();
            var item = cart.FirstOrDefault(i => i.ProductId == productId);
            if (item != null)
            {
                item.Quantity = quantity;
                SaveCart(cart);
            }
        }

    }
}
