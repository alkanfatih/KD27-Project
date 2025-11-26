using Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ProductDetail : BaseEntity
    {
        public ProductDetail()
        {
            
        }
        public ProductDetail(string description, int productId, string? features = null, string? usage=null, string? warranty = null)
        {
            Description = description;
            Features = features;
            Usage = usage;
            Warranty = warranty;
            ProductId = productId;
        }

        public string Description { get; private set; }
        public string? Features { get; private set; }
        public string? Usage { get; private set; }
        public string? Warranty { get; private set; }

        public int ProductId { get; private set; }
        public virtual Product Product { get; private set; }

        public void UpdateDetail(string description, int productId, string? features = null, string? usage = null, string? warranty = null)
        {
            Description = description;
            Features = features;
            Usage = usage;
            Warranty = warranty;
            ProductId = productId;
            UpdateDate = DateTime.Now;
        }
        public void Update(string description, string? features, string? usage, string? warranty)
        {
            Description = description;
            Features = features;
            Usage = usage;
            Warranty = warranty;
            UpdateDate = DateTime.UtcNow;
        }
    }
}
