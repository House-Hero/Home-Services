using BLL.Interface;
using Microsoft.AspNetCore.Mvc;
using DAL.Models;
using DAL.VM;

namespace HouseHero.Controllers
{
    public class SavedProviderController : Controller
    {
        private readonly ISavedProviderRepository SavedProvider;
        
        public SavedProviderController(ISavedProviderRepository savedProvider)
        {
            SavedProvider = savedProvider;
           
        }
        //SavedProvider/GetAll/1
        public IActionResult GetAll(int id , int pagenumber = 1)
        {
            int pagesize = 10;
            var savedproviders = SavedProvider.SavedProviderWithProviderwithService();
            List<Provider> List = new List<Provider>();
            foreach (var item in savedproviders)
            {
                if (item.Provider != null && item.CustomerId == id)
                    List.Add(item.Provider);
            }
            List<Service> Service = new List<Service>();
            foreach (var item in List)
            {
                Service.Add(item.Service);
            }
            ViewBag.CustomerId = id;
            int totalitem = List.Count();
            var list = List.Skip((pagenumber - 1) * pagesize).Take(pagesize).ToList();
            var model = new PaginatedVM<Provider>
            {
                item = list,
                Services = Service.Distinct().ToList(),
                Pagesize = pagesize,
                pagenum = pagenumber,
                Totalitem = totalitem
            };
            return View(model);
        }
        //sorted
        public IActionResult sorted(int id , int serviceid , int pagenumber = 1)
        {
            int pagesize = 2;
            var savedproviders = SavedProvider.SavedProviderWithProviderwithService();
            List<Provider> List = new List<Provider>();
            List<Provider> List1 = new List<Provider>();
            foreach (var item in savedproviders)
            {
                if (item.Provider != null && item.CustomerId == id )
                    List.Add(item.Provider);
            }
            List<Service> Service = new List<Service>();
            foreach (var item in List)
            {
                Service.Add(item.Service);
                if (item.ServiceId == serviceid || serviceid == 0)
                    List1.Add(item);
            }
            ViewBag.CustomerId = id;
            ViewBag.Serviceid = serviceid;
            int totalitem = List1.Count();
            var list = List1.Skip((pagenumber - 1) * pagesize).Take(pagesize).ToList();
            var model = new PaginatedVM<Provider>
            {
                item = list,
                Services = Service.Distinct().ToList(),
                Pagesize = pagesize,
                pagenum = pagenumber,
                Totalitem = totalitem
            };
            return View(model);
        }
        //href="/SavedProvider/Delete?CustomrId=@ViewBag.CustomerId&ProviderId=@Provider.Id"
        public IActionResult Delete(int CustomerId , int ProviderId)
        {
            var savedproviders = SavedProvider.GetAll();
            foreach(var item in savedproviders)
            {
                if (item.CustomerId == CustomerId && item.ProviderId == ProviderId)
                    SavedProvider.Delete(item);
            }
           
            return RedirectToAction ("GetAll",new { id = CustomerId });
        }
    }
}
