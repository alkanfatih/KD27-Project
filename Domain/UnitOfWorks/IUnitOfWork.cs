using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UnitOfWorks
{
    public interface IUnitOfWork
    {
        IProductRepository ProductRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IOrderRepository OrderRepository { get; }
        IOrderItemRepository OrderItemRepository { get; }
        IAddressRepository AddressRepository { get; }
        IProductImageRepository ProductImageRepository { get; }
        IProductDetailRepository ProductDetailRepository { get; }
        IShoppingCartRepository ShoppingCartRepository { get; }
        IAgencyApplicationRepo AgencyApplicationRepository { get; }

        Task<int> SaveChangesAscyn();
        int SaveChanges();
    }
}
