using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static Shared.Common.Enum;

namespace Domain.Dtos
{
    public  class ProductResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public double OriginalPrice { get; set; }
        public double SalePrice { get; set; }
        public Currency Currency { get; set; }
        public int SoldNumber { get; set; }
        public Category Category { get; set; }
        public List<Image> image { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsArchived { get; set; }

    }
    public class ProductRequesDto
    {
        public Guid Id { get; set; }
        [Required (ErrorMessage = "Name is required")]
        [StringLength(1000,ErrorMessage = "Name cannot be longer than 1000 characters.")]
        [RegularExpression(@"^[a-zA-Z0-9\s\-,\(\)\'\""/]*$", ErrorMessage = "Name cannot contain special characters.")]
        public string Name { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9\s\-,\(\)\.\%\!""/:]*$", ErrorMessage = "Description cannot contain special characters.")]
        [StringLength(999, ErrorMessage = "Name cannot be longer than 999 characters.")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Quantity is required")]
        [Range(1, 99999, ErrorMessage = "Age must be a natural number between 1 and 99999.")]

        public int Quantity { get; set; }

        [Required(ErrorMessage = "OriginalPrice is required")]
        [Range(1, 9999999999, ErrorMessage = "OriginalPrice must be a natural number between 1 and 9999999999.")]

        public double OriginalPrice { get; set; }
        public double SalePrice { get; set; }

        [Required(ErrorMessage = "Currency is required")]
        public Currency Currency { get; set; }
        public List<Image>? image { get; set; } 
        public int SoldNumber { get; set; }
        public Guid CategoryId { get; set; }
    }
}
