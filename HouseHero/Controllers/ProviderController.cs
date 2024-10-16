using BLL.Interface;
using HouseHero.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HouseHero.Controllers
{
    public class ProviderController : Controller
    {
        private readonly IProviderRepository _provider;
        private readonly ICustomerRepository _customer;

        public ProviderController(IProviderRepository provider,ICustomerRepository customer)
        {
            _provider = provider;
            _customer = customer;
        }
        public IActionResult Details(int id)
        {
            var Result = _provider.GetProviderWithAllRelatedData(id);
            ProviderWithAllDataViewModel ViewModel = Result;
            var applicationUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var customerId = _customer.GetCustomerByApplicationUserId(int.Parse(applicationUserId));
            var CheckSave=_customer.GetSaved(customerId.Id);
            foreach (var item in CheckSave)
            {
                if (customerId.Id == item.CustomerId && ViewModel.ProviderId == item.ProviderId)
                {
                    ViewModel.Save = true;
                    break;
                }
            }
            ViewBag.CustomerId = customerId.Id;
            return View(ViewModel);
        }
        [HttpPost]
        public IActionResult SavedProvider(int customerId, int providerId, bool isSaved)
        {
            if (isSaved)
            {
                _customer.SaveProviders(customerId, providerId);
            }
            else
            {
                _customer.UnSaveProviders(customerId,providerId);
            }

            return Json(new { success = true });
        }

    }
}
