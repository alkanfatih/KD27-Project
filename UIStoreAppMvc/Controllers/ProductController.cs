using Application.UnitOfWorks;
using Domain.RequestParameters;
using Microsoft.AspNetCore.Mvc;
using UIStoreAppMvc.Models;

namespace UIStoreAppMvc.Controllers
{
    public class ProductController : Controller
    {
        private readonly IServiceUnit _services;

        public ProductController(IServiceUnit services)
        {
            _services = services;
        }

        public async Task<IActionResult> Index(ProductRequestParameters p)
        {
            var products = await _services.ProductService.GetAllWithParametersAsync(p);

            switch (p.SortOrder)
            {
                case "price_asc":
                    products = products.OrderBy(x => x.Price);
                    break;
                case "price_desc":
                    products = products.OrderByDescending(x => x.Price);
                    break;
                case "name_asc": 
                    products = products.OrderBy(x => x.Price);
                    break;
                case "name_desc":
                    products = products.OrderByDescending(x => x.Price);
                    break;
                default:
                    break;
            }

            var productCount = await _services.ProductService.GetProductCount(p.CategoryId);

            var pagination = new PaginationInfo()
            {
                CurrentPage = p.PageNumber,
                PageSize = p.PageSize,
                Controller = "Product",
                TotalItems = productCount
            };

            return View(new ProductListViewModel() { Products= products, Pagination=pagination });

        }
    }
}
