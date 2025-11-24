using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistance.Contracts
{
    public class OrderRepository : EfRepository<Order>, IOrderRepository
    {
        public OrderRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Order?> GetByIdWithItemsAsync(int orderId)
        {
            return await _dbSet
                .Include(o => o.OrderItems)
                .Include(o => o.Address)
                .FirstOrDefaultAsync(o => o.Id == orderId && !o.IsDeleted);
        }

        public async Task<IEnumerable<Order>> GetOrdersWithItemsByUserIdAsync(int userId)
        {
            return await _dbSet
                .Include(o => o.OrderItems)
                .Include(o => o.Address)
                .Where(o => o.CustomerId == userId && !o.IsDeleted && o.Status != OrderStatus.Cancelled ).ToListAsync();
        }
    }
}
