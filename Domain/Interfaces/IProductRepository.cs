using Domain.Entities;
using Domain.RequestParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<Product?> GetByIdWithCategoryAsync(int id);
        Task<IEnumerable<Product>> GetListByCategoryIdAsync(int categoryId);
        Task<IEnumerable<Product>> GetRecentProductsAsync(int count);
        Task<IEnumerable<Product>> GetRandomFeaturedProductsAsync(int count);
        Task<IEnumerable<Product>> GetAllProductsWithParametersAsync(ProductRequestParameters parameters);
        Task<Product?> GetByIdWithDetailsAsync(int id);
        Task<List<Product>> GetTopSellingProductsAsync(int count);

    }
}
