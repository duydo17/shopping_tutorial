using System.ComponentModel.DataAnnotations;

namespace Shopping_Tutorial.Models.ViewModel
{
    public class LoginViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "nhập User name")]
        public string UserName { get; set; }
       
        [DataType(DataType.Password), Required(ErrorMessage = "Nhập password")]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
    }
}
