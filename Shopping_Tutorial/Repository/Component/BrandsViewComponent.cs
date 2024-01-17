using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shopping_Tutorial.Models;

namespace Shopping_Tutorial.Repository.Component
{
    public class CategoriesViewComponent:ViewComponent
    {
        private readonly DatabaseContext _dbContext;
        public CategoriesViewComponent(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }   
        public async Task<IViewComponentResult> InvokeAsync() =>View(await _dbContext.Categories.ToListAsync());
    }
}
