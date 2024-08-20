using static Shared.Common.Enum;

namespace Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public double OriginalPrice { get; set; }
        public double SalePrice { get; set; }
        public Currency Currency { get; set; }
        public int SoldNumber { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public List<Image> Images { get; set; }
        public List<Order> Orders { get; set; }
        public List<Like> Likes { get; set; }
    }
}
