using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistance.Extensions
{
    public static class ProductRepositoryExtension
    {
        public static IQueryable<Product> GetTakeProducts(this IQueryable<Product> query, int count=12, bool orderByAsc = true)
        {
            if(orderByAsc)
                return query.OrderBy(p => p.CreatedDate).Take(count);
            else
                return query.OrderByDescending(p => p.CreatedDate).Take(count);
        }

        public static IQueryable<Product> GetRandomFeaturedProducts(this IQueryable<Product> products, int count = 12)
        { 
            var deneme = products.Where(p => p.IsFeatured == true).OrderBy(r => Guid.NewGuid()).Take(count);
            return deneme;
        }

        public static IQueryable<Product> FilteredByCategoryId(this IQueryable<Product> products, int? categoryId)
        { 
            return categoryId.HasValue ? products.Where(p => p.CategoryId == categoryId) : products;
        }

        public static IQueryable<Product> FilteredBySearchTerm(this IQueryable<Product> products, string? searchTerm)
        { 
            if(string.IsNullOrWhiteSpace(searchTerm))
                return products;

            return products.Where(p => p.Name.ToLower().Contains(searchTerm.ToLower()));
        }

        public static IQueryable<Product> FilteredByPrice(this IQueryable<Product> products, int minPrice, int maxPrice)
        { 
            bool isValidPrice = minPrice <= maxPrice;
            return isValidPrice ? products.Where(p => p.Price >= minPrice && p.Price <= maxPrice) : products;
        }

        public static IQueryable<Product> FilteredOrder(this IQueryable<Product> products, string sortOrder)
        {
            return sortOrder switch
            {
                "name" => products.OrderBy(p => p.Name),
                "-name" => products.OrderByDescending(p => p.Name),
                "price" => products.OrderBy(p => p.Price),
                "-price" => products.OrderByDescending(p => p.Price),
                _ => products.OrderBy(p => p.Id)
            };
        }

        public static IQueryable<Product> ToPaginate(this IQueryable<Product> products, int pageNumber, int pageSize)
        { 
            return products.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }
    }
}
