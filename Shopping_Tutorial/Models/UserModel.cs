using System.ComponentModel.DataAnnotations;

namespace Shopping_Tutorial.Models
{
	public class UserModel
	{
		public int Id { get; set; }
		[Required(ErrorMessage ="nhập User name")]
		public string UserName { get; set; }
		[Required(ErrorMessage = "nhập User Email")]
		[EmailAddress]
		public string Email { get; set; }
		[DataType(DataType.Password), Required(ErrorMessage ="Nhập password")]
		public string Password { get; set; }
	}
}
