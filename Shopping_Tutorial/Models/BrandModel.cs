using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Shopping_Tutorial.Models
{
    public class BrandModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required, MinLength(4, ErrorMessage = "Yêu Cầu Nhập Tên Thương Hiệu")]
        public string Name { get; set; }
        [Required, MinLength(4, ErrorMessage = "Yêu Cầu Nhập Mô Tả Thương Hiệu")]
        public string Description { get; set; }
        [Required]
        public string Slug { get; set; }
        public int Status { get; set; }
    }
}
