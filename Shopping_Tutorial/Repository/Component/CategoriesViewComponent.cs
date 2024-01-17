using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shopping_Tutorial.Models;

namespace Shopping_Tutorial.Repository.Component
{
    public class BrandsViewComponent:ViewComponent
    {
        private readonly DatabaseContext _dbContext;
        public BrandsViewComponent(DatabaseContext dbContext)
        {
			_dbContext = dbContext;
        }   
        public async Task<IViewComponentResult> InvokeAsync() =>View(await _dbContext.Brands.ToListAsync());
    }
}
