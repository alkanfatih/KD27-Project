using Domain.Commons;

namespace Domain.Entities
{
    public class ProductImage : BaseEntity
    {
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public string ImageUrl { get; set; }

        public ProductImage(int productId, string imageUrl)
        {
            ProductId = productId;
            ImageUrl = imageUrl;
        }
    }
}
