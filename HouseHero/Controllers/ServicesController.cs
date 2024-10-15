using Microsoft.AspNetCore.Mvc;

namespace HouseHero.Controllers
{
    public class ServicesController : Controller
    {
        public IActionResult Index(int id)
        {

            return View();
        }
    }
}
