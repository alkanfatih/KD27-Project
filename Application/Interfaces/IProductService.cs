using Application.DTOs.ProductDTOs;
using Domain.RequestParameters;

namespace Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllAsync();
        Task<IEnumerable<ProductDto>> GetAllDeletedAsync();
        Task<ProductDto?> GetByIdAsync(int id);
        Task<UpdateProductDto?> GetUpdateDtoByIdAsync(int id);
        Task<IEnumerable<ProductSummaryDto>> GetSummariesAsync();
        Task AddAsync(CreateProductDto dto);
        Task UpdateAsync(UpdateProductDto dto);
        Task DeleteAsync(int id);
        Task SoftDeleteAsync(int id);
        Task RestoreAsync(int id);
        Task UpdateIsFeatured(int id, bool isFeatured);
        Task<List<RecentProductDto>> GetRecentProductsAsync(int count);
        Task<IEnumerable<ProductDto>> GetFeaturedProductsAsync(int count);
        Task<IEnumerable<ProductDto>> GetAllWithParametersAsync(ProductRequestParameters parameters);
        Task<int> GetProductCount(int? catId = null);
        Task<ProductDetailDto> GetByIdWithDetailsAsync(int id);
        Task<List<ProductListDto>> GetRecommendedProductsAsync(int count = 4);
        Task<List<ProductListDto>> GetSimilarProductsAsync(int categoryId, int excludeProductId, decimal price, int count = 4);
        Task<IEnumerable<ProductDto>> SearchProductsAsync(string name);
        Task<IEnumerable<ProductDto>> LowStockProducts(int stock);

    }
}
