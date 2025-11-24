using Domain.Entities;
using Domain.Interfaces;
using Domain.RequestParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistance.Contracts
{
    public class ProductRepository : EfRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }

        public Task<IEnumerable<Product>> GetAllProductsWithParametersAsync(ProductRequestParameters parameters)
        {
            throw new NotImplementedException();
        }

        public Task<Product?> GetByIdWithCategoryAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Product?> GetByIdWithDetailsAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetListByCategoryIdAsync(int categoryId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetRandomFeaturedProductsAsync(int count)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetRecentProductsAsync(int count)
        {
            throw new NotImplementedException();
        }

        public Task<List<Product>> GetTopSellingProductsAsync(int count)
        {
            throw new NotImplementedException();
        }
    }
}
