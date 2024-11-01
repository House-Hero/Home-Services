using BLL.Interface;
using DAL.Data.Context;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace BLL.Repository
{
    public class RequestRepository : GenericRepository<Requests>, IRequestRepository
    {

        private readonly ApplicationDbContext _context;

        public RequestRepository(ApplicationDbContext appDbContext) : base(appDbContext)
        {
            _context = appDbContext;
        }

        public IQueryable<object> GetFilterRequestsForProvider(int providerId, int? selectedStatus)
        {
            // Retrieve requests for the specific provider
            var filteredRequests = _context.Requests
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
            var result = filteredRequests.Select(r => new
            {
                CustomerName = r.Customer.ApplicationUser.Name,
                CustomerAddress = r.Customer.ApplicationUser.Address,
                CustomerPhone = r.Customer.ApplicationUser.PhoneNumber,
                RequestDate = r.RequestDate.ToString("dd MMM yyyy"),
                StatusName = r.Status.ToString(),
                PreferredCommunication = r.PreferredCommunication.ToString(),
                Comment = r.Comment,
                RequestId = r.Id
            }).AsQueryable(); ;

            return result;
        }
        public IQueryable<object> GetFilterRequestsForCustomer(int CustomerId, int? selectedStatus, int? selectedService)
        {
            // Retrieve requests for the specific customer
            var filteredRequests = _context.Requests
                 .Include(r => r.Provider)
                 .ThenInclude(p => p.ApplicationUser)
                 .Include(s => s.Service)
                 .Where(r => r.CustomerId == CustomerId);

            // Apply filters
            if (selectedStatus.HasValue)
            {
                filteredRequests = filteredRequests.Where(r => (int)r.Status == selectedStatus.Value);
            }
            if (selectedService.HasValue)
            {
                filteredRequests = filteredRequests.Where(r => r.ServiceId == selectedService.Value);
            }
            // Select the data needed for the view
            var result = filteredRequests.Select(r => new
            {
                ProviderPicture = r.Provider.ApplicationUser.ProfilePicture_ID,
                ProviderName = r.Provider.ApplicationUser.UserName,
                ProviderAddress = r.Provider.ApplicationUser.Address,
                ProviderPhone = r.Provider.ApplicationUser.PhoneNumber,
                RequestDate = r.RequestDate.ToString("dd MMM yyyy"),
                ServiceName=r.Service.Name,
                StatusName = r.Status.ToString(),
                PreferredCommunication = r.PreferredCommunication.ToString(),
                RequestId = r.Id
            }).AsQueryable(); ;

            return result;
        }

        public void ChangeStatusIntoAccept(Requests request)
        {
            request.Status = Status.Accepted; // Update the status to Rejected
            _context.SaveChanges();
        }
        public void ChangeStatusIntoCompleted(Requests request)
        {
            request.Status = Status.Completed; // Update the status to Rejected
            _context.SaveChanges();
        }
    }
}
