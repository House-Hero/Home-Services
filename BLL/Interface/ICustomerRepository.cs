using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface
{
    public interface ICustomerRepository:IGenericRepository<Customer>
    {
        public void SaveProviders(int CustomerId, int ProviderId);
        public void UnSaveProviders(int CustomerId, int ProviderId);
        public IEnumerable<SavedProvider> GetSaved(int CustomerId);
        public Customer GetCustomerByApplicationUserId(int applicationUserId);
        public void SaveRequest(Requests requests);
        public Customer GetCustomerById(int CustomerId);


    }
}
