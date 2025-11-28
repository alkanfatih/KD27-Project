using Application.DTOs.CategoryDTOs;
using Application.UnitOfWorks;
using Microsoft.AspNetCore.Mvc;

namespace UIStoreAppMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IServiceUnit _services;

        public CategoryController(IServiceUnit services)
        {
            _services = services;
        }

        //GET: /Admin/Category/Index
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var categories = await _services.CategoryService.GetAllAsync();
                return View(categories);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Kategoriler yüklenemedi! \n" + ex.Message;
                return View(new List<CategoryDto>());
            }
        }

        //GET: /Admin/Category/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryDto model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                await _services.CategoryService.AddAsync(model);
                TempData["SuccessMessage"] = "Kategori başarıyla eklendi.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Kategoriler eklenemedi! \n" + ex.Message;
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            try
            {
                var category = await _services.CategoryService.GetByIdAsync(id);
                if (category == null)
                {
                    TempData["ErrorMessage"] = "Kategori bulunamadı!";
                    return RedirectToAction(nameof(Index));
                }

                var updateDto = new UpdateCategoryDto() { Id = category.Id, Name = category.Name };

                return View(updateDto);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Kategoriler yüklenemedi! \n" + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateCategoryDto model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                await _services.CategoryService.UpdateAsync(model);
                TempData["SuccessMessage"] = "Kategori başarıyla güncellendi.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Kategoriler güncellenemedi! \n" + ex.Message;
                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _services.CategoryService.SoftDeleteAsync(id);
                TempData["SuccessMessage"] = "Kategori başarıyla silindi.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Kategoriler silinemedi! \n" + ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Trash()
        {
            try
            {
                var deletedCategories = await _services.CategoryService.GetAllDeletedAsync();
                return View(deletedCategories);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Silinen kategoriler yüklenirken bir hata oluştu! \n" + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Restore(int id)
        {
            try
            {
                await _services.CategoryService.RestoreAsync(id);
                TempData["SuccessMessage"] = "Kategori başarıyla geri yüklendi.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Kategori geri yüklenirken bir hata oluştu! \n" + ex.Message;
            }

            return RedirectToAction(nameof(Trash));
        }

        [HttpPost]
        public async Task<IActionResult> HardDelete(int id)
        {
            try
            {
                await _services.CategoryService.DeleteAsync(id);
                TempData["SuccessMessage"] = "Kategori başarıyla kalıcı olarak silind.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Kategori kalıcı bir şekilde silinirken hata oluştu! \n" + ex.Message;
            }
            return RedirectToAction(nameof(Trash));
        }
    }
}
