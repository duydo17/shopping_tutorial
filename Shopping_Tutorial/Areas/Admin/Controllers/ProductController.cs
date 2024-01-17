using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shopping_Tutorial.Models;

namespace Shopping_Tutorial.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly DatabaseContext _dbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(DatabaseContext dbContext, IWebHostEnvironment webHostEnvironment)
        {
            _dbContext = dbContext;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> Index()
        {
            var products = await _dbContext.Products.Include(x => x.Category).Include(a => a.Brand).ToListAsync();
            return View(products);
        }
        public async Task<IActionResult> Create()
        {
            var categories = await _dbContext.Categories.ToListAsync();
            var brands = await _dbContext.Brands.ToListAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name", "Id");
            ViewBag.Brands = new SelectList(brands, "Id", "Name", "Id");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductModel product)
        {
            var categories = await _dbContext.Categories.ToListAsync();
            var brands = await _dbContext.Brands.ToListAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name", "Id");
            ViewBag.Brands = new SelectList(brands, "Id", "Name", "Id");
            if (!ModelState.IsValid)
            {


                if (product.ImageUpload != null)
                {
                    string uploadsDir = Path.Combine(_webHostEnvironment.WebRootPath, "media/products");
                    string imageName = Guid.NewGuid().ToString() + "_" + product.ImageUpload.FileName;
                    string filePath = Path.Combine(uploadsDir, imageName);
                    using (var FileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await product.ImageUpload.CopyToAsync(FileStream);
                    }
                    product.Image = "media/products/" + imageName;

                }
                await _dbContext.Products.AddAsync(product);
                await _dbContext.SaveChangesAsync();
                TempData["success"] = "Thêm Sản Phẩm Thành Công";
                return RedirectToAction("Index");


            }

            return View();
        }
        public async Task<IActionResult> Edit(int id)
        {
            var categories = await _dbContext.Categories.ToListAsync();
            var brands = await _dbContext.Brands.ToListAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name", "Id");
            ViewBag.Brands = new SelectList(brands, "Id", "Name", "Id");
            var product = await _dbContext.Products.FindAsync(id);
            return View(product);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ProductModel product)
        {
            if (!ModelState.IsValid)
            {
                if (product.ImageUpload != null)
                {
                    string uploadsDir = Path.Combine(_webHostEnvironment.WebRootPath, "media/products");
                    string imageName = Guid.NewGuid().ToString() + "_" + product.ImageUpload.FileName;
                    string filePath = Path.Combine(uploadsDir, imageName);
                    using (var FileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await product.ImageUpload.CopyToAsync(FileStream);
                    }                  

                    if (!string.IsNullOrEmpty(product?.Image))
                    {
                        var pathImage = Path
                                .Combine(_webHostEnvironment.WebRootPath, product.Image);
                        if (System.IO.File.Exists(pathImage))
                        {
                            System.IO.File.Delete(pathImage);
                        }
                    }
                    product.Image = "media/products/" + imageName;
                }
                _dbContext.Products.Update(product);
                 await _dbContext.SaveChangesAsync();
                return RedirectToAction("Index");

            }
            return View(product);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _dbContext.Products.FindAsync(id);
            if (product != null)
            {
                if (!string.IsNullOrEmpty(product.Image))
                {
                    var pathImage = Path
                            .Combine(_webHostEnvironment.WebRootPath, product.Image);
                    if (System.IO.File.Exists(pathImage))
                    {
                        System.IO.File.Delete(pathImage);
                    }
                }
                _dbContext.Products.Remove(product);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
