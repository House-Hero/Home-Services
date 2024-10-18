using BLL.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HouseHero.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository Category;

        public CategoryController(ICategoryRepository category)
        {
            Category = category;
        }
        public IActionResult GetAll()
        {
            var categories = Category.GetAll();
            return View(categories);
        }
        
        public IActionResult Details(int id)
        {
            var category = Category.GetCategoryWithServicesAndProviders(id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category); 
        }
       
    }
}
