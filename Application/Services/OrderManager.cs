using Application.DTOs.OrderDTOs;
using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Domain.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class OrderManager : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<OrderDto>> GetAllAsync()
        {
            var orders = await _unitOfWork.OrderRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<OrderDto>>(orders);
        }

        public async Task<int> CreateOrderAsync(CreateOrderDto dto)
        {
            // Kullanıcının adresi kontrol edilir
            var address = await _unitOfWork.AddressRepository.GetByIdAsync(dto.AddressId);
            if (address is null || address.CustomerId != dto.UserId)
                throw new Exceptions.BusinessRuleViolationException("Geçersiz adres.");

            // Siparişi oluştur
            var order = new Order(dto.UserId, dto.AddressId, dto.TotalPrice);

            foreach (var itemDto in dto.Items)
            {
                var product = await _unitOfWork.ProductRepository.GetByIdAsync(itemDto.ProductId);
                if (product is null)
                    throw new Exceptions.BusinessRuleViolationException($"Ürün bulunamadı: {itemDto.ProductId}");

                if (product.Stock < itemDto.Quantity)
                    throw new Exceptions.BusinessRuleViolationException($"{product.Name} ürününde yeterli stok yok.");

                // Stoktan düş
                product.DecreaseStock((uint)itemDto.Quantity);

                // Siparişe ekle
                order.AddItem(product.Id, product.Name, product.Price, itemDto.Quantity);
            }

            await _unitOfWork.OrderRepository.AddAsync(order);
            await _unitOfWork.SaveChangesAscyn();

            return order.Id;
        }

        public async Task<IEnumerable<OrderDto>> GetAllByCustomerIdAsync(int custormerId)
        {
            //var orders = await _orderRepository.GetAllAsync(o => o.UserId == userId); Düzenle
            var orders = await _unitOfWork.OrderRepository.FindConditionAsync(o => o.CustomerId == custormerId);
            return _mapper.Map<IEnumerable<OrderDto>>(orders);
        }

        public async Task<OrderDto?> GetByIdAsync(int id)
        {
            var order = await _unitOfWork.OrderRepository.GetByIdWithItemsAsync(id);
            if (order == null)
                throw new NotFoundException($"Sipariş bulunamadı. (id: {id})");

            var orderItems = _mapper.Map<List<OrderItemDto>>(order.OrderItems);
            OrderDto orderDto = _mapper.Map<OrderDto>(order);
            orderDto.Items = orderItems.ToList();
            return orderDto;
        }

        public async Task CancelOrderAsync(int id)
        {
            var order = await _unitOfWork.OrderRepository.GetByIdAsync(id); //Düzenle
            if (order == null)
                throw new NotFoundException($"İptal edilecek sipariş bulunamadı. (id: {id})");

            //order.Cancel();
            _unitOfWork.OrderRepository.Update(order);
            await _unitOfWork.SaveChangesAscyn();
        }

        public async Task UpdateStatusAsync(int id, int newStatus)
        {
            var order = await _unitOfWork.OrderRepository.GetByIdAsync(id);
            if (order == null)
                throw new NotFoundException("Sipariş bulunamadı.");

            order.SetStatus((OrderStatus)newStatus); // domain metoduyla durumu güncelle
            _unitOfWork.OrderRepository.Update(order);
            await _unitOfWork.SaveChangesAscyn();
        }
    }
}
