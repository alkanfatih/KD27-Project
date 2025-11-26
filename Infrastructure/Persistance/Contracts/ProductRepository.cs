using Domain.Entities;
using Domain.Interfaces;
using Domain.RequestParameters;
using Infrastructure.Persistance.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistance.Contracts
{
    public class ProductRepository : EfRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Product>> GetAllProductsWithParametersAsync(ProductRequestParameters parameters)
        {
            return await _context
                 .Products
                 .FilteredByCategoryId(parameters.CategoryId)
                 .FilteredBySearchTerm(parameters.SearchTerm)
                 .FilteredByPrice(parameters.MinPrice, parameters.MaxPrice)
                 .ToPaginate(parameters.PageNumber, parameters.PageSize)
                 .FilteredOrder(parameters.SortOrder).ToListAsync();
        }

        public async Task<Product?> GetByIdWithCategoryAsync(int id)
        {
            return await _dbSet
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);
        }

        public async Task<Product?> GetByIdWithDetailsAsync(int id)
        {
            return await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Images)
                .Include(p => p.Detail)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Product>> GetListByCategoryIdAsync(int categoryId)
        {
            return await _dbSet
                .Where(p => p.CategoryId == categoryId && !p.IsDeleted)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetRandomFeaturedProductsAsync(int count)
        {
            return await _dbSet.GetRandomFeaturedProducts(count).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetRecentProductsAsync(int count)
        {
            return await _dbSet.GetTakeProducts(count, false).ToListAsync();
        }

        public async Task<List<Product>> GetTopSellingProductsAsync(int count)
        {
            var topProductIds = await _context.OrderItems
                .GroupBy(oi => oi.ProductId)
                .Select(g => new
                {
                    ProductId = g.Key,
                    TotalSold = g.Sum(x => x.Quantity)
                })
                .OrderByDescending(g => g.TotalSold)
                .Take(count)
                .Select(g => g.ProductId)
                .ToListAsync();

            return await _context.Products
                .Where(p => topProductIds.Contains(p.Id))
                .ToListAsync();
        }
    }
}
