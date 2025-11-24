using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.OrderDTOs
{
    public record OrderDto : BaseDto
    {
        public int CustomerId { get; init; }
        public string? CustomerFullName { get; init; }  // Mapping'te eklenecek
        public int AddressId { get; init; }
        public string? AddressSummary { get; init; } // Mapping'te eklenecek
        public DateTime OrderDate { get; init; }
        public decimal TotalPrice { get; init; }
        public string Status { get; init; }
        public List<OrderItemDto> Items { get; set; } = new();
    }
}
