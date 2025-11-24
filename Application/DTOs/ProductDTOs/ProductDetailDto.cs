using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.ProductDTOs
{
    public record ProductDetailDto
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public decimal Price { get; init; }
        public int Stock { get; init; }
        public string ImageUrl { get; init; }
        public string CategoryName { get; init; }
        public int CategoryId { get; init; }

        // Detay bilgileri
        public string? Description { get; init; }
        public string? Features { get; init; }
        public string? Usage { get; init; }
        public string? Warranty { get; init; }

        public List<string> GalleryImages { get; init; } = new();
    }
}
