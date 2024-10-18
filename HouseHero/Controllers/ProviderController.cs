using BLL.Interface;
using DAL.Data.Context;
using DAL.Models;
using HouseHero.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace HouseHero.Controllers
{

    //(Customer POV)
    public class ProviderController : Controller
    {
        private readonly IProviderRepository _provider;
        private readonly ICustomerRepository _customer;
 

        public ProviderController(IProviderRepository provider,ICustomerRepository customer )
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

      [HttpGet]
        public IActionResult RequestService(int id) 
        {
            var applicationUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var customerId = _customer.GetCustomerByApplicationUserId(int.Parse(applicationUserId));
            var serviceId = _provider.GetServiceIdForProvider(id); 

            ViewBag.ProviderId = id;
            ViewBag.CustomerId = customerId.Id;
            ViewBag.ServiceId = serviceId;

            return View(new RequestServiceViewModel());
        }

        [HttpPost]
        public IActionResult RequestService(RequestServiceViewModel requestServiceVM)
        {
            if (ModelState.IsValid)
            {
                var request = new Requests
                {
                    ProviderId = requestServiceVM.ProviderId,
                    CustomerId = requestServiceVM.CustomerId,
                    ServiceId = requestServiceVM.ServiceId,
                    RequestDate = DateTime.Now,
                    PreferredCommunication = requestServiceVM.PreferredCommunication,
                    Comment = requestServiceVM.Comment,
                    Status = Status.on_Review 
                };
                _customer.SaveRequest(request);
                return RedirectToAction("Details", new { id = requestServiceVM.ProviderId });
            }
            ModelState.AddModelError("", "Failed to add request.");
            return View(requestServiceVM);
        }
        [HttpPost]
        public IActionResult SaveReview(string Comment, int CustomerId, int ProviderId, int Rating)
        {
            var review = new Review
            {
                Comment = Comment,
                CustomerId = CustomerId,
                ProviderId = ProviderId,
                Rating = Rating
            };
            _provider.AddReview(review);
            int id = ProviderId;
            return RedirectToAction("Details", new { id = id });
        }
       
    }
}
