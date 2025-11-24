using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.AdressDTOs
{
    public record CreateAddressDto
    {
        public string Title { get; init; }
        public string Country { get; init; }
        public string PostalCode { get; init; }
        public string Line { get; init; }
        public string City { get; init; }
        public string District { get; init; }
        public int UserId { get; init; }
    }
}
