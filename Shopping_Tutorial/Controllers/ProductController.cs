using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shopping_Tutorial.Models;

namespace Shopping_Tutorial.Controllers
{
    public class ProductController : Controller
    {
		private readonly DatabaseContext _dbContext;
		public ProductController(DatabaseContext dbContext)
		{
			_dbContext = dbContext;
		}
		public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> ProductDetail(int id)
        {
            var product = await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == id);
            return View(product);
        }
    }
}
