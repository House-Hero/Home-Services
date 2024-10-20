using BLL.Interface;
using DAL.Models;
using HouseHero.Models.ViewModels.Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace HouseHero.Controllers
{
    [Authorize(Roles = "Provider")]
    public class ProviderProfileController : Controller
    {
        private readonly IProviderRepository _provider;
        private readonly ICustomerRepository _customer;
        private readonly ICloudinaryService _cloudinary;

        public ProviderProfileController(IProviderRepository provider, ICustomerRepository customer, ICloudinaryService cloudinary)
        {
            _provider = provider;
            _customer = customer;
            _cloudinary = cloudinary;
        }
        public IActionResult Details(string DefaultView = "Details")
        {
            var applicationUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var Provider = _provider.GetProviderByApplicationUserId(int.Parse(applicationUserId));
            var Result = _provider.GetProviderWithAllRelatedData(Provider.Id);
            TempData["ServiceId"] = Result.ServiceId;
            TempData["City"] = Result.ApplicationUser.CityId;
            ProviderWithAllDataViewModel ViewModel = Result;
            ViewModel.ApplactionUserId = int.Parse(applicationUserId);
            TempData["ImageUrl"] = ViewModel.ProfilePicture_ID;
            TempData["ApplactionUserId"] = ViewModel.ApplactionUserId;
            return View(DefaultView, ViewModel);
        }

        public IActionResult Edit(int id)
        {
            Details("Edit");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int Id, ProviderWithAllDataViewModel Edit)
        {
            Edit.ProviderId = Id;
            Edit.ProfilePicture_ID = TempData["ImageUrl"] != null ? TempData["ImageUrl"].ToString() : null;
            Edit.ApplactionUserId = TempData["ApplactionUserId"] != null ? (int)TempData["ApplactionUserId"] : 0;
            Provider provider = Edit;
            provider.ServiceId = TempData["ServiceId"] != null ? (int)TempData["ServiceId"] : 0;
            ApplicationUser user = Edit;
            user.CityId = TempData["City"] != null ? (int)TempData["City"] : 0;
            if (Edit.ProfilePicture_ID == null && Edit.Image != null)
                user.ProfilePicture_ID = await _cloudinary.UploadImageAsync(Edit.Image);
            else if (Edit.ProfilePicture_ID != null && Edit.Image != null)
            {
                string url = Edit.ProfilePicture_ID;
                // Regular expression to match the ID part of the URL
                string pattern = @"\/([^\/]+)\.jpg$";
                Match match = Regex.Match(url, pattern);
                if (match.Success)
                {
                    string ProfileID = match.Groups[1].Value;
                    user.ProfilePicture_ID = await _cloudinary.UpdateImageAsync(ProfileID, Edit.Image);
                }
            }

            _provider.UpdateProviderApplactionUser(provider,user);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult PortfolioDetails()
        {
            return View();
        }
        [HttpPost]
        public IActionResult PortfolioDetails(PortfolioDetails portfolioDetails)
        {

            return View();
        }
    }



}

