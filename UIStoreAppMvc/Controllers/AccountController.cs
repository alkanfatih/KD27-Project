using Application.UnitOfWorks;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UIStoreAppMvc.Models;

namespace UIStoreAppMvc.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<Customer> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly SignInManager<Customer> _signInManager;
        private readonly IServiceUnit _services;

        public AccountController(UserManager<Customer> userManager, RoleManager<IdentityRole<int>> roleManager, SignInManager<Customer> signInManager, IServiceUnit services)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _services = services;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if(!ModelState.IsValid)
                return View(model);

            var user = new Customer(model.FullName, model.Email);

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded) 
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(model);
            }

            //Role
            await _userManager.AddToRoleAsync(user, "Customer");

            await _signInManager.SignInAsync(user, isPersistent: false);

            return RedirectToAction("Welcome", "Account");
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null) 
        { 
            if(!ModelState.IsValid)
                return View();

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

            if (result.Succeeded) 
            { 
                var user = await _userManager.FindByEmailAsync(model.Email);
                var roles = await _userManager.GetRolesAsync(user);

                //MergeSessiona(Daha önce sepetine eklediği ürünleri getir.)

                if (roles.Contains("Customer") && !roles.Contains("CorporateCustomer") && !roles.Contains("Agency"))
                    return RedirectToAction("Welcome", "Account");

                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    return RedirectToAction(returnUrl);

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Geçersiz giriş bilgileri");
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        { 
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Authorize(Roles = "Customer")]
        public IActionResult Welcome() => View();
    }
}
