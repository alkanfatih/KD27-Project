using Domain.Commons;
using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class CartItem : BaseEntity
    {
        private int _quantity;

        public int ShoppingCartId { get; set; }
        public virtual ShoppingCart ShoppingCart { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public decimal Price { get; set; }
        public int Quantity 
        {
            get { return _quantity; }
            private set 
            {
                if (value < 0)
                    throw new DomainValidationException("Adet 0 dan küçük olamaz!");
                _quantity = value;
            } 
        }

        public CartItem()
        {
            
        }
        public CartItem(int productId, int quantity)
        {
            ProductId = productId;
            Quantity = quantity;
        }

        public void IncreaseQuantity(int amount)
        {
            Quantity += amount;
            UpdateDate = DateTime.UtcNow;  
        }
        public void DeccreaseQuantity(int amount)
        {
            Quantity -= amount;
            UpdateDate = DateTime.UtcNow;
        }
        public void UpdateQuantity(int amount)
        {
            Quantity = amount;
            UpdateDate = DateTime.UtcNow;
        }
    }
}
