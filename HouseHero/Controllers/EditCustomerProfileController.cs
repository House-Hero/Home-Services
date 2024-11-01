using BLL.Interface;
using DAL.Models;
using DAL.VM;
using HouseHero.Models.ViewModels.Customer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
namespace HouseHero.Controllers
{
    public class EditCustomerProfileController : Controller
    {
        private readonly ICustomerRepository Customer;
        private readonly ICityRepository City;
        private readonly ICloudinaryService _cloudinary;

        public EditCustomerProfileController(ICustomerRepository customer,ICityRepository city, ICloudinaryService cloudinary)
        {
            Customer = customer;
            City = city;
            _cloudinary = cloudinary;
        }
        public IActionResult Edit(int id)
        {
            Customer customer = Customer.GetAllCustomerDetiles(id);
            EditCustomerViewModel viewModel = customer;
            TempData["ImageUrl"] = viewModel.ImageUrl;
            viewModel.Cities = City.GetAll().ToList();
            return View(viewModel);
        }
        public async Task<IActionResult> SaveChange(int id ,EditCustomerViewModel Edit)
        {
            Edit.CustomerId = id;
            Edit.ImageUrl = TempData["ImageUrl"] != null ? TempData["ImageUrl"].ToString() : null;
            Customer customer = Edit;
            ApplicationUser user = Edit;
            if (Edit.ImageUrl == null && Edit.Image != null)
                user.ProfilePicture_ID= await _cloudinary.UploadImageAsync(Edit.Image);
            else if(Edit.ImageUrl != null && Edit.Image != null)
            {
                string url = Edit.ImageUrl;
                // Regular expression to match the ID part of the URL
                string pattern = @"\/([^\/]+)\.jpg$";
                Match match = Regex.Match(url, pattern);
                if (match.Success)
                {
                    string ProfileID = match.Groups[1].Value;
                    user.ProfilePicture_ID = await _cloudinary.UpdateImageAsync(ProfileID, Edit.Image);
                }
            }
            Customer.UpdateCustomerApplactionUser( user);
            return RedirectToAction ("GetAll","Category");
        }
    }
}
