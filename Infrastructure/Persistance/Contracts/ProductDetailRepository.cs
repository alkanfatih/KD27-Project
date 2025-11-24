using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistance.Contracts
{
    public class ProductDetailRepository : EfRepository<ProductDetail>, IProductDetailRepository
    {
        public ProductDetailRepository(AppDbContext context) : base(context)
        {
        }
    }
}
