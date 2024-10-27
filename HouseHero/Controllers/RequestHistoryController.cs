using BLL.Interface;
using DAL.Data.Context;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HouseHero.Controllers
{
    [Authorize(Roles = "Provider")]
    public class RequestHistoryController : Controller
    {
        private readonly IProviderRepository providerRepository;
        private readonly IRequestRepository requestRepository;

        public RequestHistoryController(IProviderRepository providerRepository,IRequestRepository requestRepository) 
        {
            this.providerRepository = providerRepository;
            this.requestRepository = requestRepository;
        }


        public IActionResult GetRequests(int id)
        {
            //get provider
            var provider = providerRepository.Get(id);

            // Populate status dropdown
            ViewBag.StatusList = new SelectList(
                Enum.GetValues(typeof(Status))
                .Cast<Status>()
                .Where(s => s != Status.rejected)
                .Select(s => new { Value = (int)s, Text = s.ToString() }),
                "Value", "Text");

            return View(provider);
        }


        [HttpPost]
        public IActionResult FilterRequests(int providerId, int? selectedStatus)
        {
            // Retrieve requests for the specific customer
            var filteredRequests = requestRepository.GetFilterRequests(providerId, selectedStatus);

            return Json(filteredRequests);
        }
        [HttpPost]
        public IActionResult CancelRequest(int requestId)
        {
            var request = requestRepository.Get(requestId);
            if (request != null && request.Status == Status.on_Review)
            {
                requestRepository.Delete(request);
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost]
        public IActionResult AcceptRequest(int requestId)
        {
            var request = requestRepository.Get(requestId);
            if (request != null && request.Status == Status.on_Review)
            {
                requestRepository.ChangeStatusIntoAccept(request); // Update the status to Rejected
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost]
        public IActionResult CompleteRequest(int requestId)
        {
            var request = requestRepository.Get(requestId);
            if (request != null && request.Status == Status.Accepted)
            {
                requestRepository.ChangeStatusIntoCompleted(request); // Update the status to Rejected
                return Ok();
            }
            return BadRequest();
        }

    }
}
