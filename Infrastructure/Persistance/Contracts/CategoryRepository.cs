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
    public class CategoryRepository : EfRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Category>> GetTopCategoriesAsync(int take)
        {
            return await _dbSet
                .Where(c => !c.IsDeleted)
                .OrderByDescending(c => c.Products.Count())
                .Take(take)
                .ToListAsync();
        }

        public async Task<Category?> GetWithProductsAsync(int categoryId)
        {
            return await _dbSet
                .Include(c => c.Products)
                .FirstOrDefaultAsync(c => c.Id == categoryId && !c.IsDeleted);
        }
    }
}
