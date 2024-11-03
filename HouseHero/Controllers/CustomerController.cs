using Azure.Core;
using BLL.Interface;
using BLL.Repository;
using DAL.Data.Context;
using DAL.Models;
using HouseHero.Models;
using HouseHero.Models.ViewModels;
using HouseHero.Models.ViewModels.Customer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace HouseHero.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IRequestRepository _requestRepository;
        private readonly ICityRepository _cityRepository;
        private readonly ICloudinaryService _cloudinary;

        public CustomerController(IServiceRepository serviceRepository, ICustomerRepository customerRepository,
                                  IRequestRepository requestRepository, ICityRepository cityRepository,
                                  ICloudinaryService cloudinary)
        {
            _serviceRepository = serviceRepository;
            _customerRepository = customerRepository;
            _requestRepository = requestRepository;
            _cityRepository = cityRepository;
            _cloudinary = cloudinary;
        }

        [HttpGet]
        public IActionResult CustomerProfile()
        {
            try
            {
                var applicationUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var applicationUser = _customerRepository.GetApplicationUserByApplicationUserId(applicationUserId);
                var customer = _customerRepository.GetCustomerByApplicationUserId(applicationUser.Id);

                ViewBag.StatusList = new SelectList(
                    Enum.GetValues(typeof(Status))
                    .Cast<Status>()
                    .Select(s => new { Value = (int)s, Text = s.ToString() }),
                    "Value", "Text");

                ViewBag.ServiceList = new SelectList(_serviceRepository.GetAll(), "Id", "Name");

                return View(customer);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                var req = HttpContext.TraceIdentifier;
                var errorModel = new ErrorViewModel { RequestId = req };
                return View("Error", errorModel);
            }
        }

        [HttpPost]
        public IActionResult FilterRequests(int customerId, int? selectedStatus, int? selectedService, int page = 1, int pageSize = 8)
        {
            try
            {
                var filteredRequests = _requestRepository.GetFilterRequestsForCustomer(customerId, selectedStatus, selectedService)
                                                    .Skip((page - 1) * pageSize)
                                                    .Take(pageSize)
                                                    .ToList();
                var totalRequests = _requestRepository.GetFilterRequestsForCustomer(customerId, selectedStatus, selectedService).Count();
                var totalPages = (int)Math.Ceiling((double)totalRequests / pageSize);

                return Json(new { requests = filteredRequests, totalPages });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                var req = HttpContext.TraceIdentifier;
                var errorModel = new ErrorViewModel { RequestId = req };
                return View("Error", errorModel);
            }
        }

        [HttpPost]
        public IActionResult CancelRequest(int requestId)
        {
            try
            {
                var request = _requestRepository.Get(requestId);
                if (request != null && request.Status == Status.on_Review)
                {
                    _requestRepository.Delete(request);
                    return Ok();
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                var req = HttpContext.TraceIdentifier;
                var errorModel = new ErrorViewModel { RequestId = req };
                return View("Error", errorModel);
            }
        }

        public IActionResult Edit(int id)
        {
            try
            {
                Customer customer = _customerRepository.GetAllCustomerDetiles(id);
                EditCustomerViewModel viewModel = customer;
                TempData["ImageUrl"] = viewModel.ImageUrl;
                viewModel.Cities = _cityRepository.GetAll().ToList();
                return View(viewModel);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                var req = HttpContext.TraceIdentifier;
                var errorModel = new ErrorViewModel { RequestId = req };
                return View("Error", errorModel);
            }
        }

        public async Task<IActionResult> SaveChange(int id, EditCustomerViewModel Edit)
        {
            try
            {
                Edit.CustomerId = id;
                Edit.ImageUrl = TempData["ImageUrl"] != null ? TempData["ImageUrl"].ToString() : null;
                Customer customer = Edit;
                ApplicationUser user = Edit;

                if (Edit.ImageUrl == null && Edit.Image != null)
                    user.ProfilePicture_ID = await _cloudinary.UploadImageAsync(Edit.Image);
                else if (Edit.ImageUrl != null && Edit.Image != null)
                {
                    string url = Edit.ImageUrl;
                    string pattern = @"\/([^\/]+)\.jpg$";
                    Match match = Regex.Match(url, pattern);
                    if (match.Success)
                    {
                        string ProfileID = match.Groups[1].Value;
                        user.ProfilePicture_ID = await _cloudinary.UpdateImageAsync(ProfileID, Edit.Image);
                    }
                }
                _customerRepository.UpdateCustomerApplactionUser(user);
                return RedirectToAction("CustomerProfile");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                var req = HttpContext.TraceIdentifier;
                var errorModel = new ErrorViewModel { RequestId = req };
                return View("Error", errorModel);
            }
        }
    }
}
