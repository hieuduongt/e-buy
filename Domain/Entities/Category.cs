namespace Domain.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string UrlPath { get; set; }
        public List<Product> Products { get; set; }
    }
}
