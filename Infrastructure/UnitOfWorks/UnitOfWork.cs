using Domain.Interfaces;
using Domain.UnitOfWorks;
using Infrastructure.Persistance;
using Infrastructure.Persistance.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IProductRepository ProductRepository => new ProductRepository(_context);

        public ICategoryRepository CategoryRepository => new CategoryRepository(_context);

        public IOrderRepository OrderRepository => new OrderRepository(_context);

        public IOrderItemRepository OrderItemRepository => new OrderItemRepository(_context);

        public IAddressRepository AddressRepository => new AddressRepository(_context);

        public IProductImageRepository ProductImageRepository => new ProductImageRepository(_context);

        public IProductDetailRepository ProductDetailRepository => new ProductDetailRepository(_context);

        public IShoppingCartRepository ShoppingCartRepository => new ShoppingCartRepository(_context);

        public IAgencyApplicationRepo AgencyApplicationRepository => new AgencyApplicationRepository(_context);

        public int SaveChanges()
        {
            var result = _context.SaveChanges();

            if (result == 0)
                throw new Exception("Değişiklik kayıt edilemedi");
            return result;
        }

        public async Task<int> SaveChangesAscyn()
        {
            var result = await _context.SaveChangesAsync();

            if (result == 0)
                throw new Exception("Değişiklik kayıt edilemedi");
            return result;
        }
    }
}
