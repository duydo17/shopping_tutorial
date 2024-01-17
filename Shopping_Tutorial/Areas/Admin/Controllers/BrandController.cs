using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shopping_Tutorial.Models;

namespace Shopping_Tutorial.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BrandController : Controller
    {
        private readonly DatabaseContext _dbContext;
        public BrandController(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IActionResult> Index()
        {
            var brands = await _dbContext.Brands.ToListAsync();

            return View(brands);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult>Create(BrandModel brand) { 
            await _dbContext.Brands.AddAsync(brand);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult>Edit(int id)
        {
            var brand = await _dbContext.Brands.FirstOrDefaultAsync(b => b.Id == id);
            return View(brand);
        }
        [HttpPost]
        public async Task<IActionResult>Edit(BrandModel brand)
        {
            _dbContext.Brands.Update(brand);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult>Delete(int id)
        {
            var brand = _dbContext.Brands.FirstOrDefault(b => b.Id == id);
            _dbContext.Brands.Remove(brand);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
