using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.ProductDTOs
{
    public record UpdateProductDto : BaseDto
    {
        [Required]
        public string Name { get; init; }

        public string? Description { get; init; }

        [Range(0.01, 999999)]
        public decimal Price { get; init; }

        [Range(0, int.MaxValue)]
        public int Stock { get; init; }

        [Range(0, 1)]
        public decimal Discount { get; init; }

        public int CategoryId { get; init; }

        public string? CurrentImageUrl { get; init; } // Mevcut görselin ismi

        public IFormFile? NewImageFile { get; init; } // Yeni görsel yüklenecekse
    }
}
