using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shopping_Tutorial.Models;

namespace Shopping_Tutorial.Controllers
{
	public class BrandController : Controller
	{
		private readonly DatabaseContext _dbContext;
		public BrandController(DatabaseContext dbContext)
		{
			_dbContext = dbContext;
		}
		public async Task<IActionResult> Index(int id)
		{
			BrandModel brand = await _dbContext.Brands.FirstOrDefaultAsync(x => x.Id == id);
			if (brand == null)
			{
				return RedirectToAction("Index");
			}
			var productByBrand = await _dbContext.Products.Where(x => x.BrandId == brand.Id)
				.OrderByDescending(c => c.Id).ToListAsync();
			return View(productByBrand);
		}
	}
}
