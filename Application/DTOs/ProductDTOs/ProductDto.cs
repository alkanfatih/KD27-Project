using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.ProductDTOs
{
    public record ProductDto : BaseDto
    {
        public string Name { get; init; }
        public string? Description { get; init; }
        public decimal Price { get; init; }
        public int Stock { get; init; }
        public decimal Discount { get; init; }
        public string? ImageUrl { get; init; }
        public int CategoryId { get; init; }
        public bool IsFeatured { get; init; }
    }
}
