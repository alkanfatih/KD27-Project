using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UnitOfWorks
{
    public interface IServiceUnit
    {
        ICategoryService CategoryService { get; }
        IProductService ProductService { get; }
        IOrderService OrderService { get; }
        ICustomerService CustomerService { get; }
        IAddressService AddressService { get; }
        IProductImageService ProductImageService { get; }
        IProductDetailService ProductDetailService { get; }
        IShoppingCartService ShoppingCartService { get; }
        ISessionCartService SessionCartService { get; }
        IAgencyApplicationService AgencyApplicationService { get; }
    }
}
