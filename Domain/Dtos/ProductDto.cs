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
    public class ProductResponseDto
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
        public List<Image> Images { get; set; }
        public List<Like> Likes { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsArchived { get; set; }


    }
    public class ProductRequesDto
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Tên Không được để trống !")]
        [StringLength(1000, ErrorMessage = "Tên không được dài quá 100 kí tự.")]
        [RegularExpression(@"^[a-zA-Z0-9\s\-,\(\)\'\""/]*$", ErrorMessage = "Tên không được chứa kí tự dặc biệt.")]
        public string Name { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9\s\-,\(\)\.\%\!""/:]*$", ErrorMessage = "Miêu tả sản phẩm không được chứa kí tự dặc biệt.")]
        [StringLength(999, ErrorMessage = "Miêu tả sản phẩm không được vượt quá 999 kí tự .")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Số lượng không được để trống ! ")]
        [Range(1, 99999, ErrorMessage = "Số lượng phải lớn hơn 0 , giới hạn là 9999 sản phẩm .")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Giá gốc không được để trống ! ")]
        [Range(1, 9999999999, ErrorMessage = "Giá gốc phải lớn hơn 0 ")]
        public double OriginalPrice { get; set; }

        [Required(ErrorMessage = "giá bán không được để trống ! ")]
        [Range(1, 9999999999, ErrorMessage = "Giá bán phải lớn hơn 0 ")]
        public double SalePrice { get; set; }

        [Required(ErrorMessage = "Đơn vị tiền không được bỏ trống ! ")]
        public Currency Currency { get; set; }
        public List<Image>? Images { get; set; }
        public Guid CategoryId { get; set; }
    }
}
