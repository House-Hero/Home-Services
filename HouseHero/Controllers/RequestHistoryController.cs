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
        private ApplicationDbContext ApplicationDb;


        public RequestHistoryController(ApplicationDbContext applicationDb) 
        {
            this.ApplicationDb = applicationDb;
        }


        public IActionResult GetRequests(int id)
        {
            //get provider
            var provider = ApplicationDb.Providers.FirstOrDefault(x => x.Id == id);
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
            var filteredRequests = ApplicationDb.Requests
                .Include(r => r.Customer)
                .ThenInclude(p => p.ApplicationUser)
                .Include(s => s.Service)
                .Where(r => r.ProviderId == providerId && r.Status != Status.rejected);

            // Apply status filter if a status is selected
            if (selectedStatus.HasValue)
            {
                filteredRequests = filteredRequests.Where(r => (int)r.Status == selectedStatus.Value);
            }

            // Select the data needed for the view
            var result = filteredRequests.Select(r => new {
                CustomerName = r.Customer.ApplicationUser.Name,
                CustomerAddress = r.Customer.ApplicationUser.Address,
                CustomerPhone = r.Customer.ApplicationUser.PhoneNumber,
                RequestDate = r.RequestDate.ToString("dd MMM yyyy"),
                StatusName = r.Status.ToString(),
                PreferredCommunication=r.PreferredCommunication.ToString(),
                Comment = r.Comment,
                RequestId = r.Id
            }).ToList();

            return Json(result);
        }
        [HttpPost]
        public IActionResult CancelRequest(int requestId)
        {
            var request = ApplicationDb.Requests.FirstOrDefault(i => i.Id == requestId);
            if (request != null && request.Status == Status.on_Review)
            {
                request.Status = Status.rejected; // Update the status to Rejected
                ApplicationDb.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost]
        public IActionResult AcceptRequest(int requestId)
        {
            var request = ApplicationDb.Requests.FirstOrDefault(i => i.Id == requestId);
            if (request != null && request.Status == Status.on_Review)
            {
                request.Status = Status.Accepted; // Update the status to Rejected
                ApplicationDb.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost]
        public IActionResult CompleteRequest(int requestId)
        {
            var request = ApplicationDb.Requests.FirstOrDefault(i => i.Id == requestId);
            if (request != null && request.Status == Status.Accepted)
            {
                request.Status = Status.Completed; // Update the status to Rejected
                ApplicationDb.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }

    }
}
