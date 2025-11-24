using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.CartItemDTOs
{
    public record AdminCartDto
    {
        public int CustomerId { get; init; }
        public string CustomerName { get; init; }
        public List<AdminCartItemDto> Items { get; init; } = new();

        public decimal TotalPrice => Items.Sum(i => i.UnitPrice * i.Quantity);
        public int TotalItemCount => Items.Sum(i => i.Quantity);
    }
}
