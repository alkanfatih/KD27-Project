using Application.UnitOfWorks;
using Microsoft.AspNetCore.Mvc;
using UIStoreAppMvc.Models;

namespace UIStoreAppMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly IServiceUnit _services;

        public HomeController(IServiceUnit services)
        {
            _services = services;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var featured = await _services.ProductService.GetFeaturedProductsAsync(8);
            return View(featured);
        }
    }
}
