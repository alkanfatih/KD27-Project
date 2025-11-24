using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistance.Contracts
{
    public class OrderItemRepository : EfRepository<OrderItem>, IOrderItemRepository
    {
        public OrderItemRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<OrderItem>> GetAllByOrderIdAsync(int orderId)
        {
            return await _dbSet.Where(i => i.OrderId == orderId && !i.IsDeleted).ToListAsync();
        }

        public async Task<IEnumerable<OrderItem>> GetAllByProductIdAsync(int productId)
        {
            return await _dbSet.Where(i => i.ProductId == productId && !i.IsDeleted)
                .Include(i => i.Order)
                .ToListAsync();
        }
    }
}
