namespace Domain.Dtos
{
    public class CategoryRequestDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string UrlPath { get; set; }
        public string Description { get; set; }
    }

    public class CategoryResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string UrlPath { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsArchived { get; set; }
    }
}
