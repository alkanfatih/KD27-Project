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
    public class ProductImageRepository : EfRepository<ProductImage>, IProductImageRepository
    {
        public ProductImageRepository(AppDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<ProductImage>> GetAllByProductIdAsync(int productId)
        {
            return await _dbSet.Where(i => i .ProductId == productId && !i.IsDeleted).ToListAsync();
        }

        public async Task<ProductImage?> GetMainImageAsync(int productId)
        {
            return await _dbSet.Where(i => i.ProductId == productId && !i.IsDeleted)
                .OrderBy(i => i.Id)
                .FirstOrDefaultAsync();
        }
    }
}
