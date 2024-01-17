using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shopping_Tutorial.Models
{
    public class ProductModel
    {
        [Key]
        
        public int Id { get; set; }
        [Required, MinLength(4, ErrorMessage = "Yêu Cầu Nhập Tên Sản Phẩm")]
        public string Name { get; set; }
        [Required, MinLength(4, ErrorMessage = "Yêu Cầu Nhập Mô Tả Sản Phẩm")]
        public string Description { get; set; }
        public string Slug {  get; set; }
        [Column(TypeName ="decimal(10, 2)")]
        public decimal Price { get; set; }
        public string? Image {  get; set; }
        public int BrandId { get; set; }
        public int CategoryId { get; set;}
        public CategoryModel? Category { get; set; }
        public BrandModel? Brand { get; set; }
        [NotMapped]
        [FileExtensions]
        public IFormFile? ImageUpload { get; set; }
    }
}
