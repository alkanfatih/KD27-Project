using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.ProductDetailsDTOs
{
    public record UpdateProductDetailDto
    {
        [Required]
        public int ProductId { get; init; }
        [Required]
        public string Description { get; init; }

        public string? Features { get; init; }
        public string? Usage { get; init; }
        public string? Warranty { get; init; }
    }
}
