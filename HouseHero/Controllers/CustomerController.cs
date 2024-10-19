
using Azure.Core;
using BLL.Interface;
using DAL.Data.Context;
using DAL.Models;
using HouseHero.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HouseHero.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        private readonly ApplicationDbContext ApplicationDb;
        private readonly IServiceRepository _serviceRepository;

        public CustomerController(ApplicationDbContext context, IServiceRepository serviceRepository)
        {
            ApplicationDb = context;
            _serviceRepository = serviceRepository;
        }
        [HttpGet]
        public IActionResult CustomerProfile(string UserName)
        {
            // Fetch customer data
            var applicationUser = ApplicationDb.Users.Include(x => x.City).FirstOrDefault(x => x.UserName == UserName);
            var customer = ApplicationDb.Customers.Include(x => x.ApplicationUser)
                .FirstOrDefault(x => x.ApplicationUserId == applicationUser.Id);

            // Populate status dropdown
            ViewBag.StatusList = new SelectList(
                Enum.GetValues(typeof(Status))
                .Cast<Status>()
                .Select(s => new { Value = (int)s, Text = s.ToString() }),
                "Value", "Text");

            // Populate services dropdown
            ViewBag.ServiceList = new SelectList(ApplicationDb.Services, "Id", "Name");

            return View(customer);
        }

        [HttpPost]
        public IActionResult FilterRequests(int customerId, int? selectedStatus, int? selectedService)
        {
            // Retrieve requests for the specific customer
            var filteredRequests = ApplicationDb.Requests
                .Include(r => r.Provider)
                .ThenInclude(p => p.ApplicationUser)
                .Include(s => s.Service)
                .Where(r => r.CustomerId == customerId);

            // Apply status filter if a status is selected
            if (selectedStatus.HasValue)
            {
                filteredRequests = filteredRequests.Where(r => (int)r.Status == selectedStatus.Value);
            }

            // Apply service filter if a service is selected
            if (selectedService.HasValue)
            {
                filteredRequests = filteredRequests.Where(r => r.ServiceId == selectedService.Value);
            }

            // Select the data needed for the view
            var result = filteredRequests.Select(r => new {
                ProviderName = r.Provider.ApplicationUser.UserName,
                ProviderAddress = r.Provider.ApplicationUser.Address,
                ProviderPhone = r.Provider.ApplicationUser.PhoneNumber,
                ServiceName = r.Service.Name,
                RequestDate = r.RequestDate.ToString("dd MMM yyyy"),
                StatusName = r.Status.ToString(),
                RequestId =r.Id
            }).ToList();

            return Json(result);
        }
        [HttpPost]
        public IActionResult CancelRequest(int requestId)
        {
            var request = ApplicationDb.Requests.FirstOrDefault(i => i.Id == requestId);
            if (request != null && request.Status == Status.on_Review)
            {
                ApplicationDb.Requests.Remove(request); // Update the status to Rejected
                ApplicationDb.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }

    }







}