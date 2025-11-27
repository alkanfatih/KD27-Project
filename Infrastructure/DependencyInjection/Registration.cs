using Application.Interfaces;
using Application.Services;
using Application.UnitOfWorks;
using Domain.Entities;
using Domain.Interfaces;
using Domain.UnitOfWorks;
using FluentValidation;
using Infrastructure.Persistance.Contracts;
using Infrastructure.Persistance;
using Infrastructure.UnitOfWorks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DependencyInjection
{
    public static class Registration
    {
        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // DbContext Bağlantısı
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
            b => b.MigrationsAssembly("UIStoreAppMvc"));

                options.EnableSensitiveDataLogging(true);
            });

            //Identity Ayarı
            services.AddIdentity<Customer, IdentityRole<int>>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.User.RequireUniqueEmail = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireDigit = true;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

            // Repository ve UnitOfWork Registration
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public static void AddApplicationServices(this IServiceCollection services)
        {
            // AutoMapper Registration
            services.AddAutoMapper(typeof(Application.AssemblyReference).Assembly);


            // FluentValidation Registration
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            // Service kayıtları
            services.AddScoped<ICategoryService, CategoryManager>();
            services.AddScoped<IProductService, ProductManager>();
            services.AddScoped<IOrderService, OrderManager>();
            services.AddScoped<ICustomerService, CustomerManager>();
            services.AddScoped<IAddressService, AddressManager>();
            services.AddScoped<IProductImageService, ProductImageManager>();
            services.AddScoped<IProductDetailService, ProductDetailManager>();
            services.AddScoped<ISessionCartService, SessionCartService>();
            services.AddScoped<IShoppingCartService, ShoppingCartManager>();
            services.AddScoped<IAgencyApplicationService, AgencyApplicationManager>();

            // ServiceUnit kayıt
            services.AddScoped<IServiceUnit, ServiceUnit>();
        }
    }
}
