using Application.DTOs.CartItemDTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.RequestParameters;
using Domain.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ShoppingCartManager : IShoppingCartService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ShoppingCartManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<CartItemDto>> GetCartItemDtoAsync(int userId)
        {
            var cart = await _unitOfWork.ShoppingCartRepository.GetCartWithItemsAsync(userId);
            if (cart is not null)
            {
                var cartItems = cart.CartItems.Select(x => new CartItemDto { ProductId = x.ProductId, ImageUrl = x.Product.ImageUrl, Name = x.Product.Name, Quantity = x.Quantity, UnitPrice = x.Product.Price }).ToList();
                return cartItems;
            }
            else
            { return Enumerable.Empty<CartItemDto>(); }
        }

        public async Task AddItemAsync(int userId, int productId, int quantity = 1)
        {
            var cart = await GetOrCreateCartAsync(userId);
            var item = new CartItem(productId, quantity);
            cart.AddItem(item);

            _unitOfWork.ShoppingCartRepository.Update(cart);
            await _unitOfWork.SaveChangesAscyn();
        }

        public async Task ClearCartAsync(int userId)
        {
            var cart = await GetOrCreateCartAsync(userId);
            cart.Clear();

            _unitOfWork.ShoppingCartRepository.Update(cart);
            await _unitOfWork.SaveChangesAscyn();
        }

        public async Task<ShoppingCart> GetOrCreateCartAsync(int userId)
        {
            var cart = await _unitOfWork.ShoppingCartRepository.GetCartWithItemsAsync(userId);
            if (cart == null)
            {
                cart = new ShoppingCart { UserId = userId };
                await _unitOfWork.ShoppingCartRepository.AddAsync(cart);
                await _unitOfWork.SaveChangesAscyn();
            }

            return cart;
        }

        public async Task RemoveItemAsync(int userId, int productId)
        {
            var cart = await GetOrCreateCartAsync(userId);
            cart.RemoveItem(productId);

            _unitOfWork.ShoppingCartRepository.Update(cart);
            await _unitOfWork.SaveChangesAscyn();
        }

        public void Update(ShoppingCart cart)
        {
            _unitOfWork.ShoppingCartRepository.Update(cart);
            _unitOfWork.SaveChanges();
        }

        public async Task UpdateQuantityAsync(int userId, int productId, int quantity)
        {
            var cart = await GetOrCreateCartAsync(userId);
            var item = cart.CartItems.FirstOrDefault(i => i.ProductId == productId);
            item?.UpdateQuantity(quantity);

            _unitOfWork.ShoppingCartRepository.Update(cart);
            await _unitOfWork.SaveChangesAscyn();
        }

        public async Task<IEnumerable<AdminCartDto>> GetAllCartsForAdminAsync()
        {
            var carts = await _unitOfWork.ShoppingCartRepository.GetCartsWithDetailsAsync();

            return carts.Select(cart => new AdminCartDto
            {
                CustomerId = cart.User.Id,
                CustomerName = cart.User.UserName,
                Items = cart.CartItems.Select(item => new AdminCartItemDto
                {
                    ProductId = item.ProductId,
                    ProductName = item.Product.Name,
                    Quantity = item.Quantity,
                    UnitPrice = item.Product.Price
                }).ToList()
            }).ToList();
        }

        public async Task<IEnumerable<AdminCartDto>> GetFilteredCartsAsync(CartFilterParameters filters)
        {
            var carts = await _unitOfWork.ShoppingCartRepository.GetCartsWithDetailsAsync();

            var result = carts.Select(cart => new AdminCartDto
            {
                CustomerId = cart.User.Id,
                CustomerName = cart.User.UserName,
                Items = cart.CartItems.Select(item => new AdminCartItemDto
                {
                    ProductId = item.ProductId,
                    ProductName = item.Product.Name,
                    Quantity = item.Quantity,
                    UnitPrice = item.Product.Price
                }).ToList()
            });

            if (!string.IsNullOrWhiteSpace(filters.UserNameOrEmail))
                result = result.Where(c => c.CustomerName.Contains(filters.UserNameOrEmail, StringComparison.OrdinalIgnoreCase));

            if (filters.MinItemCount.HasValue)
                result = result.Where(c => c.TotalItemCount >= filters.MinItemCount);

            if (filters.MinTotalPrice.HasValue)
                result = result.Where(c => c.TotalPrice >= filters.MinTotalPrice);

            return result;
        }
    }
}
