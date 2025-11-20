using Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; private set; }

        public virtual ICollection<Product> Products { get; private set; } = new List<Product>();
        public Category() { }
        public Category(string name)
        {
            Name = name;
        }

        public void Rename(string newName)
        { 
            Name = newName;
            UpdateDate = DateTime.Now;
        }

        public void AddProduct(Product product)
        { 
            Products.Add(product); 
        }
    }
}
