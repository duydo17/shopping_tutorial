using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shopping_Tutorial.Models
{
    public class CategoryModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required,MinLength(4,ErrorMessage ="Yêu Cầu Nhập Tên Danh Mục")]
        public string Name { get; set; }
        [Required, MinLength(4, ErrorMessage = "Yêu Cầu Nhập Mô Tả Danh Mục")]
        public string Description { get; set; }
        [Required]
        public string Slug {  get; set; }
        public int Status { get; set; }
    }
}
