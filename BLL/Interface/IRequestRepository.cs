using Azure.Core;
using DAL.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface
{
    public interface IRequestRepository : IGenericRepository<Requests>
    {
        public IQueryable<object> GetFilterRequestsForProvider(int providerId, int? selectedStatus);
        public IQueryable<object> GetFilterRequestsForCustomer(int CustomerId, int? selectedStatus, int? selectedService);
        public void ChangeStatusIntoAccept(Requests request);
        public void ChangeStatusIntoCompleted(Requests request);
       
    }
}
