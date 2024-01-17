using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shopping_Tutorial.Models;
using System.Security.Cryptography.X509Certificates;

namespace Shopping_Tutorial.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly DatabaseContext _dbContext;
        public CategoryController(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IActionResult> Index()
        {
            var categories = await _dbContext.Categories.ToListAsync();

            return View(categories);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CategoryModel category) {
            await _dbContext.Categories.AddAsync(category);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit(int id) { 
        
            var category = await _dbContext.Categories.SingleOrDefaultAsync(c => c.Id == id);
            return View(category);
        }
        [HttpPost]
        public async Task<IActionResult>Edit(CategoryModel category) {
            _dbContext.Categories.Update(category);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult>Delete(int id)
        {
            var category = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id==id);
            _dbContext.Categories.Remove(category);
                await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
