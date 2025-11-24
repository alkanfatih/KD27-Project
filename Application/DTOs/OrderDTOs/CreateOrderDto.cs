using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.OrderDTOs
{
    public record CreateOrderDto
    {
        public int UserId { get; init; }
        public int AddressId { get; init; }
        public decimal TotalPrice => Items.Sum(x => x.Price * x.Quantity);
        public List<CreateOrderItemDto> Items { get; init; } = new();
    }
}
