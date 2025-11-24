using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IShoppingCartRepository : IRepository<ShoppingCart>
    {
        Task<ShoppingCart?> GetCartWithItemsAsync(int userId);
        Task<IEnumerable<ShoppingCart>> GetCartsWithDetailsAsync();
    }
}
