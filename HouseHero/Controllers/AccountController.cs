using BLL.Interface;
using BLL.Repository;
using DAL.Data.Context;
using DAL.Models;
using HouseHero.Models.ViewModels;
using HouseHero.Models.ViewModels.Customer;
using HouseHero.Models.ViewModels.Provider;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.X86;
using System.Text.Json;

namespace HouseHero.Controllers
{
    public class AccountController : Controller
    {
        private readonly IServiceRepository ServiceRepository;
        private readonly UserManager<ApplicationUser> UserManager;
        private readonly SignInManager<ApplicationUser> SignInManager;
        private readonly ApplicationDbContext ApplicationDb;
        private readonly ICityRepository CityRepository;

        public AccountController(IServiceRepository serviceRepository,UserManager<ApplicationUser> userManager,ApplicationDbContext applicationDb, ICityRepository cityRepository , SignInManager<ApplicationUser> signInManager)
        {
            ServiceRepository = serviceRepository;
            UserManager = userManager;
            ApplicationDb = applicationDb;
            CityRepository = cityRepository;
            SignInManager = signInManager;
        }


        //-------------------------------------------------------------------------------------
        
        [HttpGet]
        public IActionResult RegisterType()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RegisterType(RegisterTypeViewModel registerTypeVM)
        {
            if (registerTypeVM.IsProvider)
            {
                return RedirectToAction(nameof(RegisterProviderIdentity));
            }
            else
            {
                return RedirectToAction(nameof(RegisterCustomer));
            }
        }
        //--------------------------------------------------------------------------------

        

        //-------------------------------------------------------------------------------

        [HttpGet]
        public IActionResult RegisterProviderIdentity()
        {
            return View(new ProviderIdentityViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RegisterProviderIdentity(ProviderIdentityViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Store basic identity information (Name, Email, etc.) in TempData or session
            TempData["ProviderIdentity"] = JsonSerializer.Serialize(model);

            return RedirectToAction(nameof(RegisterProviderDetails));
        }

        //--------------------------------------------------------------------------------------

        [HttpGet]
        public IActionResult RegisterProviderDetails()
        {
            ViewBag.Services= ServiceRepository.GetAll();
            ViewBag.City= CityRepository.GetAll();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RegisterProviderDetails(ProviderDetailsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Services = ServiceRepository.GetAll();
                ViewBag.City = CityRepository.GetAll();

                return View(model);
            }

            // Save provider's address, phone number, etc.
            TempData["ProviderDetails"] = JsonSerializer.Serialize(model);

            return RedirectToAction(nameof(RegisterProviderAvailability));
        }

        //---------------------------------------------------------------------------------------------------

        [HttpGet]
        public IActionResult RegisterProviderAvailability()
        {
            return View(new ProviderAvailabilityViewModel());
        }
        [HttpPost]
        public IActionResult AddDayToSchedule(Day day, string fromTime, string toTime)
        {
            if (string.IsNullOrEmpty(fromTime) || string.IsNullOrEmpty(toTime))
            {
                return Json(new { success = false, message = "Please fill all the fields" });
            }

            // Retrieve the current list of available days from TempData
            List<Available_Day> availableDays = TempData.ContainsKey("AvailableDays")
                ? JsonSerializer.Deserialize<List<Available_Day>>(TempData["AvailableDays"].ToString())
                : new List<Available_Day>();

            // Add the new day to the list
            availableDays.Add(new Available_Day
            {
                Day = day,
                Start_Time = TimeOnly.Parse(fromTime), 
                End_Time = TimeOnly.Parse(toTime)      
            });

            // Store the updated list back in TempData
            TempData["AvailableDays"] = JsonSerializer.Serialize(availableDays);
            TempData.Keep("AvailableDays"); // Ensure that TempData persists across requests

            var model = new
            {
                Day = day,
                FromTime = fromTime,
                ToTime = toTime
            };

            return PartialView("_DaySchedulePartial", model);
        }

[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> RegisterProviderAvailability(ProviderAvailabilityViewModel model)
{
    if (!ModelState.IsValid)
    {
        return View(model);
    }

    var providerIdentity = JsonSerializer.Deserialize<ProviderIdentityViewModel>(TempData["ProviderIdentity"].ToString());
    var providerDetails = JsonSerializer.Deserialize<ProviderDetailsViewModel>(TempData["ProviderDetails"].ToString());

    var user = new ApplicationUser
    {
        Name =providerIdentity.Name,
        UserName = providerIdentity.Name,
        Email = providerIdentity.Email,
        Address = providerDetails.Address,
        PhoneNumber = providerDetails.PhoneNumber,
        Age = providerDetails.Age,
        CityId = providerDetails.CityId,
    };



    var result = await UserManager.CreateAsync(user, providerIdentity.Password);
    if (result.Succeeded)
    {
        var provider = new Provider
        {

            ApplicationUserId = user.Id,
            Bio = model.Bio,
            ServiceId = providerDetails.ServiceId,
            Available_Day = new List<Available_Day>()
        };
        ApplicationDb.Providers.Add(provider);
        await UserManager.AddToRoleAsync(user, "Provider");
        await ApplicationDb.SaveChangesAsync();

        // Retrieve available days from TempData
        var availableDays = JsonSerializer.Deserialize<List<Available_Day>>(TempData["AvailableDays"]?.ToString());

        if (availableDays != null)
        {
            foreach (var day in availableDays)
            {
                day.ProviderId = provider.Id;

                        // Check if a record with the same ProviderId, Day, Start_Time, and End_Time exists
                        //var existingDay = ApplicationDb.Available_Day
                        //    .FirstOrDefault(d => d.ProviderId == provider.Id && d.Day == day.Day
                        //                            && d.Start_Time == day.Start_Time && d.End_Time == day.End_Time);

                        //if (existingDay == null)
                        //{
                        //    // If no duplicate is found, add the new available day
                        //    ApplicationDb.Available_Day.Add(day);
                        //    await ApplicationDb.SaveChangesAsync();
                        //}
                        //else
                        //{
                        //    continue;
                        //}
                        ApplicationDb.Available_Day.Add(day);

                        await ApplicationDb.SaveChangesAsync();

                    }

                }
        // Clear TempData after use
        TempData.Remove("ProviderIdentity");
        TempData.Remove("ProviderDetails");
        TempData.Remove("AvailableDays");

        return RedirectToAction("Login");
    }

    // Log or display the errors
    foreach (var error in result.Errors)
    {
        ModelState.AddModelError("", error.Description);
    }

    // Return the view with model state errors
    return View(model);
}



        //-----------------------------------------------------------------------------------------------------
        //Error
        //An unhandled exception occurred while processing the request.
        //ArgumentNullException: Value cannot be null. (Parameter 'items')
        //System.ArgumentNullException.Throw(string paramName)
        //SelectList City = new SelectList(ViewBag.City, "Id", "Name");

        [HttpGet]
        public IActionResult RegisterCustomer()
        {
            ViewBag.City = CityRepository.GetAll();
            return View(new CustomerRegistrationViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterCustomer(CustomerRegistrationViewModel model)
        {
            if (!ModelState.IsValid)
            {

                ViewBag.City = CityRepository.GetAll();
                return View(model);
            }

            // Create customer user
            var user = new ApplicationUser
            {
                UserName = model.Name,
                Name = model.Name,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Age = model.Age,
                Address = model.Address,
                CityId = model.CityId
            };

            var result = await UserManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                var customer = new Customer
                {
                    ApplicationUserId = user.Id
                };
                ApplicationDb.Customers.Add(customer);
                await UserManager.AddToRoleAsync(user, "Customer");
                await ApplicationDb.SaveChangesAsync();

                return RedirectToAction("Login");
                // Redirect to a Login
            }

            ModelState.AddModelError("", "Failed to register customer.");
            return View(model);
        }
        //--------------------------------------------------------------

        [HttpGet]
        public IActionResult Login([FromQuery] string? returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginUserViewModel loginUserView , string? returnUrl)
        {
            if(ModelState.IsValid)
            {
                ApplicationUser applicationUser = await UserManager.FindByEmailAsync(loginUserView.Email);
                if (applicationUser != null)
                {
                    var result = await UserManager.CheckPasswordAsync(applicationUser, loginUserView.Password);
                    if (result)
                    {    
                        //create cookie
                        await SignInManager.SignInAsync(applicationUser, loginUserView.RememberMe);
                        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                            return Redirect(returnUrl);
                        else
                            return RedirectToAction("GetAll", "Category");
                    }
                }
                ModelState.AddModelError("", "Invaild User Name and password!");
            }
            ViewData["ReturnUrl"] = returnUrl;
            return View(loginUserView);
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            //damage cookie
            await SignInManager.SignOutAsync();
            return RedirectToAction("GetAll", "Category");
        }


    }

}
