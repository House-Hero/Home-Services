using BLL.Interface;
using DAL.Data.Context;
using DAL.Models;
using HouseHero.Models;
using HouseHero.Models.ViewModels;
using HouseHero.Models.ViewModels.Provider;
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

        public ProviderController(IProviderRepository provider, ICustomerRepository customer)
        {
            _provider = provider;
            _customer = customer;

        }
        public IActionResult Details(int id)
        {
            try
            {
                if (User.IsInRole("Provider"))
                {
                    var requestId = HttpContext.TraceIdentifier;
                    var errorModel = new ErrorViewModel { RequestId = requestId };
                    return View("Error", errorModel);

                }
                var Result = _provider.GetProviderWithAllRelatedData(id);
                if (Result == null)
                {
                    return NotFound("Provider not found.");
                }
                ProviderWithAllDataViewModel ViewModel = Result;
                var applicationUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var customerId = _customer.GetCustomerByApplicationUserId(int.Parse(applicationUserId));
                var CheckSave = _customer.GetSaved(customerId.Id);
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
            catch (Exception ex)
            {
                             
                var req = HttpContext.TraceIdentifier;
                var errorModel = new ErrorViewModel { RequestId = req };
                return View("Error", errorModel);
            }
        }



        [HttpPost]
        public IActionResult SavedProvider(int customerId, int providerId, bool isSaved)
        {
            try
            {
                if (isSaved)
                {
                    _customer.SaveProviders(customerId, providerId);
                }
                else
                {
                    _customer.UnSaveProviders(customerId, providerId);
                }
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred while saving the provider." });
            }

        } 

        [HttpGet]
        public IActionResult RequestService(int id)
        {
            try
            {
                var applicationUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var customerId = _customer.GetCustomerByApplicationUserId(int.Parse(applicationUserId));
                var serviceId = _provider.GetServiceIdForProvider(id);

                ViewBag.ProviderId = id;
                ViewBag.CustomerId = customerId.Id;
                ViewBag.ServiceId = serviceId;

                return View(new RequestServiceViewModel());
            }
            catch (Exception ex)
            {
                var req = HttpContext.TraceIdentifier;
                var errorModel = new ErrorViewModel { RequestId = req };
                return View("Error", errorModel);
            }
        }

        [HttpPost]
        public IActionResult RequestService(RequestServiceViewModel requestServiceVM)
        {
            if (ModelState.IsValid)
            {
                try
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
                catch (Exception ex)
                {
                    var req = HttpContext.TraceIdentifier;
                    var errorModel = new ErrorViewModel { RequestId = req };
                    return View("Error", errorModel);
                }
            }
            ModelState.AddModelError("", "Failed to add request.");
            return View(requestServiceVM);
        }


        [HttpPost]
        public IActionResult SaveReview(string Comment, int CustomerId, int ProviderId, int Rating)
        {
            try
            {
                // Retrieve the customer using the CustomerId
                var customer = _customer.GetCustomerById(CustomerId);
                if (customer == null)
                {
                    return Json(new { success = false, message = "Customer not found." });
                }

                var review = new Review
                {
                    Comment = Comment,
                    CustomerId = CustomerId,
                    ProviderId = ProviderId,
                    Rating = Rating
                };

                _provider.AddReview(review);
                //update rating 
                _provider.Update_Rating(ProviderId);

                // Return the review data as JSON for the AJAX success callback
                return Json(new { customerName = customer.ApplicationUser?.UserName ?? "Unknown", comment = review.Comment, rating = review.Rating , customerImage =customer.ApplicationUser.ProfilePicture_ID });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred while saving the review." });
            }

        }

    }
}
