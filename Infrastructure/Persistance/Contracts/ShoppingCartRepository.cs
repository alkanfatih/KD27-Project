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
    public class ShoppingCartRepository : EfRepository<ShoppingCart>, IShoppingCartRepository
    {
        public ShoppingCartRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<ShoppingCart>> GetCartsWithDetailsAsync()
        {
            return await _context.ShoppingCarts
                .Include(c => c.User)
                .Include(c => c.CartItems)
                    .ThenInclude(ci => ci.Product)
                .ToListAsync(); 
        }

        public async Task<ShoppingCart?> GetCartWithItemsAsync(int userId)
        {
            return await _context.ShoppingCarts
                .Include(c => c.CartItems)
                .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId && !c.IsDeleted);
        }
    }
}
