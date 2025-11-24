using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.ProductImageDTOs
{
    public record UpdateProductImageDto : BaseDto
    {
        public string ImageUrl { get; init; }
        public int ProductId { get; init; }
    }
}
