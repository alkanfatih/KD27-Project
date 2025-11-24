using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.ProductDTOs
{
    public record CreateProductDto
    {
        [Required(ErrorMessage = "Ürün adı boş geçilemez!")]
        public string Name { get; init; }

        public string? Description { get; init; }

        [Required(ErrorMessage = "Ürün fiyatı boş geçilemez!")]
        [Range(0.01, 999999, ErrorMessage = "Fiyat 0 ile 999999 arasında olmalıdır!")]
        public decimal Price { get; init; }

        [Required(ErrorMessage = "Ürün stok bilgisi boş geçilemez!")]
        [Range(0, int.MaxValue, ErrorMessage = "Stok 0 dan küçük olamaz!")]
        public int Stock { get; init; }

        [Required(ErrorMessage = "Ürün indirim oranı boş geçilemez!")]
        [Range(0, 1)]
        public decimal Discount { get; init; }

        public int CategoryId { get; init; }

        public IFormFile? ImageFile { get; init; } // Ana görsel
    }
}
