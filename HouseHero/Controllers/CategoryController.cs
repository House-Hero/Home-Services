using BLL.Interface;
using DAL.Models;
using DAL.VM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HouseHero.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository Category;
        private readonly ICityRepository City;
        public CategoryController(ICategoryRepository category, ICityRepository city)
        {
            Category = category;
            City = city;
        }
        public IActionResult GetAll()
        {
            var categories = Category.GetAll();
            return View(categories);
        }

        public IActionResult Details(int id, int pagenumber = 1)
        {
            int pagesize = 10;
            var category = Category.GetCategoryWithServicesAndProviders(id);
            List<Service> serviceList = category.Services.ToList();
            List<Provider> ProviderList = new List<Provider>();
            foreach (var m in serviceList)
            {
                if (m.Providers != null)
                {
                    foreach (var x in m.Providers)
                    {
                        ProviderList.Add(x);
                    }
                }
            }
            int totalitem = ProviderList.Count();
            var list = ProviderList.Skip((pagenumber - 1) * pagesize).Take(pagesize).ToList();
            ViewBag.City = City.GetAll().ToList();
            ViewBag.CategoryName = category.Name;
            ViewBag.CategoryID = category.Id;
            var model = new PaginatedVM<Provider>
            {
                item = list,
                Services = serviceList,
                Pagesize = pagesize,
                pagenum = pagenumber,
                Totalitem = totalitem
            };
            if (category == null)
            {
                return NotFound();
            }

            return View(model);

        }
        // Category/sorted
        public IActionResult sorted(int CategoryID, int serviceid, int rating, int cityid, int pagenumber = 1)
        {
            int pagesize = 2;
            var category = Category.GetCategoryWithServicesAndProviders(CategoryID);
            List<Service> serviceList = category.Services.ToList();
            if (serviceid == 0 && rating == 0 && cityid == 0)
            {
                return RedirectToAction("Details", new { id = CategoryID });
            }
            if (serviceid > 0)
            {
                serviceList = category.Services.Where(s => s.Id == serviceid).ToList();
            }
            List<Provider> ProviderList = new List<Provider>();
            foreach (var m in serviceList)
            {
                if (m.Providers != null)
                {
                    foreach (var x in m.Providers)
                    {
                        if ((x.ApplicationUser.CityId == cityid || cityid == 0) && (x.Rating == rating || rating == 0))
                            ProviderList.Add(x);
                    }
                }
            }
            int totalitem = ProviderList.Count();
            var list = ProviderList.Skip((pagenumber - 1) * pagesize).Take(pagesize).ToList();
            ViewBag.City = City.GetAll().ToList();
            ViewBag.CategoryName = category.Name;
            ViewBag.CategoryID = CategoryID;
            ViewBag.serviceid = serviceid;
            ViewBag.rating = rating;
            ViewBag.cityid = cityid;
            var model = new PaginatedVM<Provider>
            {
                item = list,
                Services = category.Services.ToList(),
                Pagesize = pagesize,
                pagenum = pagenumber,
                Totalitem = totalitem
            };
            if (category == null)
            {
                return NotFound();
            }

            return View(model);
        }

    }
}
