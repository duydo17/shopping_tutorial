using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shopping_Tutorial.Models;
using Shopping_Tutorial.Models.ViewModel;
using Shopping_Tutorial.Repository;

namespace Shopping_Tutorial.Controllers
{
	public class CartController : Controller
	{
		private readonly DatabaseContext _dbContext;
		public CartController(DatabaseContext dbContext)
		{
			_dbContext = dbContext;
		}
		public IActionResult Index()
		{
			List<CartItemModel> cartItems = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
			CartItemViewModel cartVM = new()
			{
				CartItems = cartItems,
				GrandTotal = cartItems.Sum(x => x.Quantity * x.Price)
			};
			return View(cartVM);
		}
		public async Task<IActionResult> Add(int id)
		{
			ProductModel product = await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == id);
			List<CartItemModel> cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
			CartItemModel cartItems = cart.Where(x => x.ProductId == id).FirstOrDefault();
			if (cartItems == null)
			{
				cart.Add(new CartItemModel(product));
			}
			else
			{
				cartItems.Quantity += 1;
			}
			HttpContext.Session.SetJson("Cart", cart);
			TempData["success"] = "Add Item To Cart Successfully";
			// trả về trang hiện tại
			return Redirect(Request.Headers["referer"].ToString());
		}
		public async Task<IActionResult> Decrease(int id)
		{
			List<CartItemModel> cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart");
			CartItemModel cartItem = cart.Where(x => x.ProductId == id).FirstOrDefault();
			if (cartItem.Quantity > 1)
			{
				--cartItem.Quantity;
			}
			else
			{
				cart.RemoveAll(p => p.ProductId == id);
			}
			if (cart.Count == 0)
			{
				HttpContext.Session.Remove("Cart");
			}
			else
			{
				HttpContext.Session.SetJson("Cart", cart);
			}
			TempData["success"] = "Decrease Item Quantity To Cart Successfully";
			return RedirectToAction("Index");
		}
		public async Task<IActionResult> Increase(int id)
		{
			List<CartItemModel> cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart");
			CartItemModel cartItem = cart.Where(x => x.ProductId == id).FirstOrDefault();
			if (cartItem.Quantity >= 1)
			{
				++cartItem.Quantity;

			}
			else
			{
				cart.RemoveAll(p => p.ProductId == id);
			}
			if (cart.Count == 0)
			{
				HttpContext.Session.Remove("Cart");
			}
			else
			{
				HttpContext.Session.SetJson("Cart", cart);
			}
			TempData["success"] = "Increase Item Quantity To Cart Successfully";
			return RedirectToAction("Index");
		}
		public async Task<IActionResult> Remove(int id)
		{

			List<CartItemModel> cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart");
			
				cart.RemoveAll(p => p.ProductId == id);
			if (cart.Count == 0)
			{
				HttpContext.Session.Remove("Cart");
			}
			else
			{
				HttpContext.Session.SetJson("Cart", cart);
			}
			TempData["success"] = "Remove Item of Cart Successfully";
			return RedirectToAction("Index");
		}
		public async Task<IActionResult> Clear()
		{
			HttpContext.Session.Remove("Cart");
			TempData["success"] = "Clear Item of Cart Successfully";
			return RedirectToAction("Index");
		}
		}
}
