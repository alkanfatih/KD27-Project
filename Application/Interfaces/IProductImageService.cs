using Application.DTOs.ProductImageDTOs;
using Microsoft.AspNetCore.Http;

namespace Application.Interfaces
{
    public interface IProductImageService
    {
        Task<IEnumerable<ProductImageDto>> GetAllByProductIdAsync(int productId);
        Task<ProductImageDto?> GetByIdAsync(int id);
        Task AddAsync(CreateProductImageDto dto);
        Task UpdateAsync(UpdateProductImageDto dto);
        Task DeleteAsync(int id);
        Task AddImagesAsync(int productId, List<IFormFile> files);
    }
}
