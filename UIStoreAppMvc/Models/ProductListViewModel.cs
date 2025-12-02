using Application.DTOs.ProductDTOs;
using Domain.RequestParameters;

namespace UIStoreAppMvc.Models
{
    public class ProductListViewModel
    {
        public IEnumerable<ProductDto> Products { get; set; } = Enumerable.Empty<ProductDto>();

        public PaginationInfo Pagination { get; set; } = new();
        public int TotalCount => Products.Count();
    }
}
