using Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class OrderItem : BaseEntity
    {
        public int OrderId { get; private set; }
        public virtual Order Order { get; private set; }
        public int ProductId { get; private set; }
        public virtual Product Product { get; private set; }
        public string ProductName { get; private set; }
        public decimal UnitPrice { get; private set; }
        public int Quantity { get; set; }

        public OrderItem()
        {
            
        }

        public OrderItem(int productId, string productName, int quantity, decimal unitPrice)
        {
            ProductId = productId;
            ProductName = productName;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }

        public decimal GetSubTotal() => Quantity * UnitPrice;

        public void IncreaseQuantity(int amount)
        {
            Quantity += amount;
        }

        public void DecreaseQuantity(int amount)
        {
            if(Quantity > amount)
                Quantity -= amount;
        }
    }
}
