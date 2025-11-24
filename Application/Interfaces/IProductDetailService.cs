using Application.DTOs.ProductDetailsDTOs;

namespace Application.Interfaces
{
    public interface IProductDetailService
    {
        Task<UpdateProductDetailDto?> GetByIdOrDefaultAsync(int id);
        Task<UpdateProductDetailDto?> FirstOrDefaultAsync(int id);
        Task AddAsync(UpdateProductDetailDto dto);
        Task UpdateAsync(UpdateProductDetailDto dto);
        Task DeleteAsync(int id);
        Task<bool> AnyAsync(int id);
    }
}
