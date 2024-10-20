
using Azure.Core;
using BLL.Interface;
using DAL.Data.Context;
using DAL.Models;
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

        public CustomerController( IServiceRepository serviceRepository ,ICustomerRepository customerRepository
                               , IRequestRepository requestRepository)
        {
            _serviceRepository = serviceRepository;
            _customerRepository = customerRepository;
            _requestRepository = requestRepository;
        }
        [HttpGet]
        public IActionResult CustomerProfile()
        {
            // Fetch customer data
            var applicationUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var applicationUser = _customerRepository.GetApplicationUserByApplicationUserId(applicationUserId);
            var customer = _customerRepository.GetCustomerByApplicationUserId(applicationUser.Id);

            // Populate status dropdown
            ViewBag.StatusList = new SelectList(
                Enum.GetValues(typeof(Status))
                .Cast<Status>()
                .Select(s => new { Value = (int)s, Text = s.ToString() }),
                "Value", "Text");

            // Populate services dropdown
            ViewBag.ServiceList = new SelectList(_serviceRepository.GetAll(), "Id", "Name");

            return View(customer);
        }

        [HttpPost]
        public IActionResult FilterRequests(int customerId, int? selectedStatus, int? selectedService)
        {
            // Retrieve requests for the specific customer
            var filteredRequests = _customerRepository.GetFilterRequests(customerId, selectedStatus, selectedService);  

            return Json(filteredRequests);
        }
        [HttpPost]
        public IActionResult CancelRequest(int requestId)
        {
            var request = _requestRepository.Get(requestId);
            if (request != null && request.Status == Status.on_Review)
            {
                _requestRepository.Delete(request);
                return Ok();
            }
            return BadRequest();
        }

    }







}