using Domain.Commons;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Customer : IdentityUser<int>, IBaseEntity
    {
        public string FullName { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public bool IsDeleted { get; set; } = false;

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
        public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

        public void Restore()
        {
            if (!IsDeleted)
            {
                IsDeleted = true;
                UpdateDate = DateTime.Now;
                DeletedDate = null;
            }
        }

        public void SoftDelete()
        {
            if (IsDeleted)
            {
                IsDeleted = false;
                DeletedDate = DateTime.Now;
                UpdateDate = DateTime.Now;
            }
        }
        public Customer()
        {
            
        }
        public Customer(string fullName, string email)
        {
            FullName = fullName;
            Email = email;
            UserName = email;
        }

        public void UpdateName(string fullName)
        {
            FullName = fullName;
        }

        public void AddAdress(Address adress)
        { 
            Addresses.Add(adress);
        }

        public void AddOrder(Order order)
        { 
            Orders.Add(order);
        }
    }
}
