using BLL.Interface;
using DAL.Data.Context;
using DAL.Models;
using HouseHero.Models;
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
        public IActionResult FilterRequests(int providerId, int? selectedStatus, int page = 1, int pageSize = 6)
        {
            // Retrieve paginated requests for the provider based on the selected status
            var filteredRequests = requestRepository.GetFilterRequestsForProvider(providerId, selectedStatus)
                                                    .Skip((page - 1) * pageSize)
                                                    .Take(pageSize)
                                                    .ToList();

            // Retrieve total count for pagination
            var totalRequests = requestRepository.GetFilterRequestsForProvider(providerId, selectedStatus).Count();

            return Json(new { filteredRequests, totalRequests });
        }

        [HttpPost]
        public IActionResult CancelRequest(int requestId)
        {
            var request = requestRepository.Get(requestId);
            if (request != null && request.Status == Status.on_Review)
            {
                try
                {

                    requestRepository.Delete(request);
                    return Ok();
                }
                catch (Exception ex)
                {
                    var req = HttpContext.TraceIdentifier;
                    var errorModel = new ErrorViewModel { RequestId = req };
                    return View("Error", errorModel);
                }
            }
            return BadRequest();
        }

        [HttpPost]
        public IActionResult AcceptRequest(int requestId)
        {
            var request = requestRepository.Get(requestId);
            if (request != null && request.Status == Status.on_Review)
            {
                try
                {
                    requestRepository.ChangeStatusIntoAccept(request); // Update the status to Rejected
                    return Ok();

                }
                catch (Exception ex)
                {
                    var req = HttpContext.TraceIdentifier;
                    var errorModel = new ErrorViewModel { RequestId = req };
                    return View("Error", errorModel);
                }

            }
            return BadRequest();
        }

        [HttpPost]
        public IActionResult CompleteRequest(int requestId)
        {
            var request = requestRepository.Get(requestId);
            if (request != null && request.Status == Status.Accepted)
            {

                try
                {

                    requestRepository.ChangeStatusIntoCompleted(request); // Update the status to Rejected
                    return Ok();
                }
                catch (Exception ex)
                {
                    var req = HttpContext.TraceIdentifier;
                    var errorModel = new ErrorViewModel { RequestId = req };
                    return View("Error", errorModel);
                }
            }
            return BadRequest();
        }

    }
}
