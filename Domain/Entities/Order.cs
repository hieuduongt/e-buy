using static Shared.Common.Enum;

namespace Domain.Entities
{
    public class Order : BaseEntity
    {
        public string CustomerName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public double TotalAmount { get; set; }
        public Currency Currency { get; set; }
        public List<Product> Products { get; set; }
        public Guid? UserId { get; set; }
        public User? User { get; set; }
    }
}
