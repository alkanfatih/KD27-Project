using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IOrderItemRepository : IRepository<OrderItem>
    {
        Task<IEnumerable<OrderItem>> GetAllByOrderIdAsync(int orderId);
        Task<IEnumerable<OrderItem>> GetAllByProductIdAsync(int productId);
    }
}
