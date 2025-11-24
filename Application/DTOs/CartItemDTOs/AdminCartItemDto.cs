using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.CartItemDTOs
{
    public record AdminCartItemDto
    {
        public int ProductId { get; init; }
        public string ProductName { get; init; }
        public int Quantity { get; init; }
        public decimal UnitPrice { get; init; }
        public decimal Total => UnitPrice * Quantity;
    }
}
