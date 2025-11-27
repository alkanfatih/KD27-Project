using Domain.Entities;
using Infrastructure.Persistance;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Seeds
{
    public static class DbInitializer
    {
        public static void ConfigureAndCheckMigration(this IApplicationBuilder app)
        {
            var context = app
                .ApplicationServices
                .CreateScope()
                .ServiceProvider
                .GetRequiredService<AppDbContext>();

            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
        }
        public static async void ConfigureSeedDataAsync(this IApplicationBuilder app)
        {
            var context = app
                .ApplicationServices.CreateAsyncScope()
                .ServiceProvider.GetRequiredService<AppDbContext>();

            //2.Kategorileri ekle
            if (!context.Categories.Any())
            {
                var categories = new List<Category>
            {
                new("Tükenmez Kalemler"),
                new("Kurşun Kalemler"),
                new("Dolma Kalemler"),
                new("Promosyon Kalemler")
            };
                context.Categories.AddRange(categories);
                context.SaveChanges();
            }

        }
        public static async void ConfigureDefaultAdminUserAsync(this IApplicationBuilder app)
        {
            const string adminUser = "Admin";
            const string adminPassword = "Admin+123456";

            using var scope = app.ApplicationServices.CreateScope();

            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<Customer>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();

            // Gerekli rolleri oluştur
            string[] roles = { "Customer", "CorporateCustomer", "Agency" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole<int>(role));
            }

            // Admin kullanıcıyı oluştur
            Customer user = await userManager.FindByNameAsync(adminUser);
            if (user is null)
            {
                user = new Customer(adminUser, "alkan@mail.com")
                {
                    PhoneNumber = "5061112233",
                    UserName = adminUser,
                };

                var result = await userManager.CreateAsync(user, adminPassword);

                if (!result.Succeeded)
                    throw new Exception("Admin user could not been created.");

                var roleResult = await userManager.AddToRolesAsync(user,
                    roleManager
                        .Roles
                        .Select(r => r.Name)
                        .ToList()
                );

                if (!roleResult.Succeeded)
                    throw new Exception("System have problems with role defination for admin.");
            }
        }
    }
}
