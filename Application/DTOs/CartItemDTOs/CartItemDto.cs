using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.CartItemDTOs
{
    public record CartItemDto
    {
        public int ProductId { get; init; }
        public string Name { get; init; } = string.Empty;
        public string ImageUrl { get; init; } = "Default.png";
        public decimal UnitPrice { get; init; }
        public int Quantity { get; set; }
    }
}
