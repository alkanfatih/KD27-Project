using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.OrderDTOs
{
    public record CreateOrderItemDto
    {
        public int ProductId { get; init; }
        public int Quantity { get; init; }
        public decimal Price { get; set; }
    }
}
