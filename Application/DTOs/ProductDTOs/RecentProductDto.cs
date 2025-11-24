using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.ProductDTOs
{
    public class RecentProductDto
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public DateTime CreatedDate { get; init; }
    }
}
