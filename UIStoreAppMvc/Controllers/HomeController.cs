using Microsoft.AspNetCore.Mvc;
using UIStoreAppMvc.Models;

namespace UIStoreAppMvc.Controllers
{
    public class HomeController : Controller
    {
        private static List<Product> products;

        public HomeController()
        {
            if (products == null)
                products = new List<Product>()
                {
                    new Product { Id=1, Name="Kalem-1", Price=100 },
                    new Product { Id=2, Name="Kalem-2", Price=120 },
                    new Product { Id=3, Name="Kalem-3", Price=90 },
                };
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
