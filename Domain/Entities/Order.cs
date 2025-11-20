using Domain.Commons;
using Domain.Enums;
using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Order : BaseEntity
    {
        private decimal _totalPrice;
        public string OrderNumber { get; private set; } = Guid.NewGuid().ToString("N").ToUpper().Substring(0, 5);

        public int CustomerId { get; private set; }
        public virtual Customer Customer { get; private set; }

        public int AddressId { get; private set; }
        public virtual Address Address { get; private set; }

        public DateTime OrderDate { get; private set; } = DateTime.Now;
        public decimal TotalPrice
        {
            get { return _totalPrice; }
            private set
            {
                if (value > 0)
                    _totalPrice = value;
                else
                    throw new DomainValidationException("Toplam miktar 0 veya 0 dan küçük olamaz!");
            }
        }

        public OrderStatus Status { get; private set; }
        public virtual ICollection<OrderItem> OrderItems { get; private set; } = new List<OrderItem>();

        public Order()
        {

        }

        public Order(int customerId, int addressId, decimal totalPrice)
        {
            CustomerId = customerId;
            AddressId = addressId;
            OrderDate = DateTime.Now;
            Status = OrderStatus.Pending;
            TotalPrice = totalPrice;
        }

        public void AddItem(int productId, string productName, decimal price, int quantity)
        {
            var existingItem = OrderItems.FirstOrDefault(i => i.ProductId == productId);
            if (existingItem != null)
            {
                existingItem.IncreaseQuantity(quantity);
            }
            else
            {
                OrderItems.Add(new OrderItem(productId, productName, quantity, price));
            }
        }

        public void ChangeStatus(OrderStatus status)
        {
            Status = status;
            UpdateDate = DateTime.Now;
        }

        public void CalculateTotalPrice()
        {
            TotalPrice = OrderItems.Sum(i => i.UnitPrice * i.Quantity);
        }

        public void Cancel()
        {
            Status = OrderStatus.Cancelled;
            UpdateDate = DateTime.Now;
        }


    }
}