using Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ShoppingCart : BaseEntity
    {
        public int UserId { get; set; }
        public virtual Customer User { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
        public decimal TotalPrice => CartItems.Sum(x => x.Price * x.Quantity);

        public void AddItem(CartItem item)
        {
            var existingItem = CartItems.FirstOrDefault(i => i.ProductId == item.ProductId);
            if (existingItem != null)
            {
                existingItem.IncreaseQuantity(item.Quantity);
            }
            else
            {
                CartItems.Add(item);
            }

            UpdateDate = DateTime.Now;
        }

        public void RemoveItem(int productId)
        {
            var item = CartItems.FirstOrDefault(i => i.ProductId == productId);
            if (item != null)
            {
                CartItems.Remove(item);
                UpdateDate = DateTime.Now;
            }
        }

        public void Clear()
        { 
            CartItems.Clear();
            UpdateDate = DateTime.Now;
        }
    }
}
