﻿namespace Domain.Entities
{
    public class Image : BaseEntity
    {
        public string Url { get; set; }
        public Product Product { get; set; }
        public Guid ProductId { get; set; }
    }
}
