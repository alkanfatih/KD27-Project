using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.ProductDTOs
{
    public record ProductSummaryDto : BaseDto
    {
        public string Name { get; init; }
        public decimal Price { get; init; }
        public string? ImageUrl { get; init; }
    }
}
