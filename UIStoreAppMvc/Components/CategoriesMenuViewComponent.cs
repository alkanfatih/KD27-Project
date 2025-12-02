using Application.UnitOfWorks;
using Microsoft.AspNetCore.Mvc;

namespace UIStoreAppMvc.Components
{
    public class CategoriesMenuViewComponent : ViewComponent
    {
        private readonly IServiceUnit _service;

        public CategoriesMenuViewComponent(IServiceUnit service)
        {
            _service = service;
        }

        public async Task<IViewComponentResult> InvokeAsync() 
        { 
            var categories = await _service.CategoryService.GetAllAsync();
            return View(categories);
        }
    }
}
