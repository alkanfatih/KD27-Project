using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UnitOfWorks
{
    public class ServiceUnit : IServiceUnit
    {
        public ServiceUnit(
              ICategoryService categoryService,
              IProductService productService,
              IOrderService orderService,
              ICustomerService customerService,
              IAddressService addressService,
              IProductImageService productImageService,
              IProductDetailService productDetailService,
              IShoppingCartService shoppingCartService,
              ISessionCartService sessionCartService,
              IAgencyApplicationService agencyApplicationService)
        {
            CategoryService = categoryService;
            ProductService = productService;
            OrderService = orderService;
            CustomerService = customerService;
            AddressService = addressService;
            ProductImageService = productImageService;
            ProductDetailService = productDetailService;
            ShoppingCartService = shoppingCartService;
            SessionCartService = sessionCartService;
            AgencyApplicationService = agencyApplicationService;
        }

        public ICategoryService CategoryService { get; }
        public IProductService ProductService { get; }
        public IOrderService OrderService { get; }
        public ICustomerService CustomerService { get; }
        public IAddressService AddressService { get; }
        public IProductImageService ProductImageService { get; }
        public IProductDetailService ProductDetailService { get; }
        public IShoppingCartService ShoppingCartService { get; }
        public ISessionCartService SessionCartService { get; }

        public IAgencyApplicationService AgencyApplicationService { get; }
    }
}
