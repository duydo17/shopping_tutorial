using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shopping_Tutorial.Models;

namespace Shopping_Tutorial.Controllers
{
    public class CategoryController : Controller
    {
        private readonly DatabaseContext _dbContext;
        public CategoryController(DatabaseContext dbContext)
        {
              _dbContext = dbContext;
        } 
        public async  Task<IActionResult> Index(int id)
        {
            CategoryModel category = await _dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (category == null)
            {
                return RedirectToAction("Index");
            }
            var productByCategory =  await _dbContext.Products.Where(x=>x.CategoryId == category.Id)
                .OrderByDescending(c => c.Id).ToListAsync();
            return View ( productByCategory);
        }
    }
}
