using Domain.Commons;
using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Product : BaseEntity
    {
        private decimal _price;
        private int _stock;
        private decimal _discount=1;
        public Product()
        {
            
        }
        public Product(string name, decimal price, int stock, int categoryId, string? description)
        {
            Name = name;
            Price = price;
            Stock = stock;
            CategoryId = categoryId;
            Description = description;
        }

        public Product(string name, decimal price, int stock, string image, decimal discount, bool isFutured, int categoryId, string? description)
        {
            Name = name;
            Price = price;
            Stock = stock;
            ImageUrl = image;
            Discount = discount;
            IsFeatured = isFutured;
            CategoryId = categoryId;
            Description = description;
        }

        public string Name { get; private set; }
        public string? Description { get; private set; }
        public decimal Price 
        {
            get { return _price; }
            private set 
            {
                if(value > 0)
                    _price = value;
                else
                    throw new InvalidOperationException("Fiyat 0 veya 0 dan küçük olamaz!");
            }
        }
        public int Stock 
        {
            get { return _stock; }
            private set 
            {
                if (value > 0)
                    _stock = value;
                else
                    throw new InvalidOperationException("Stok 0 veya 0 dan küçük olamaz!");
            }
        }
        public decimal Discount 
        {
            get { return _discount; }
            private set 
            {
                if (value > 0 && value <= 1)
                    _discount = value;
                else
                    throw new InvalidOperationException("Indirim oranı 1 ile 0 arasında olmalıdır!");
            }
        }

        public string ImageUrl { get; private set; } = "Default.png";
        public bool IsFeatured { get; private set; } = false;
        public int CategoryId { get; private set; }
        public virtual Category Category { get; private set; }
        public virtual ProductDetail? Detail { get; private set; }
        public virtual ICollection<ProductImage> Images { get; set; } = new List<ProductImage>();

        public void DecreaseStock(uint amount)
        {
            if (amount > Stock)
                throw new BusinessRuleException("StokKontrolü", "Yetersiz stok girişi!");
            Stock -= (int)amount;
            UpdateDate = DateTime.UtcNow;
        }

        public void IncreaseStock(uint amount)
        { 
            Stock += (int)amount;
            UpdateDate = DateTime.UtcNow;
        }

        public void SetImage(string url)
        { 
            ImageUrl = url;
            UpdateDate= DateTime.UtcNow;
        }

        public void MarkAsFeatured()
        {
            if(!IsFeatured)
            {
                IsFeatured = true;
                UpdateDate = DateTime.Now;
            } 
        }

        public void UnMarkAsFeatured()
        {
            if (IsFeatured)
            {
                IsFeatured = false;
                UpdateDate = DateTime.Now;
            }
        }

        public void UpdateDetails(string name, decimal price, int stock, string? description)
        { 
            Name = name;
            Price = price;
            Stock = stock;
            Description = description;
            UpdateDate = DateTime.Now;
        }

        public void AddProductDetail(ProductDetail detail)
        {
            Detail = detail;
        }
    }
}
