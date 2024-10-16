using Azure.Core;
using BLL.Interface;
using DAL.Data.Context;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repository
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        private readonly ApplicationDbContext _context;

        public CustomerRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public Customer GetCustomerByApplicationUserId(int applicationUserId)
        {
            
                var customer= _context.Customers
                    .FirstOrDefault(c => c.ApplicationUserId == applicationUserId);
            return customer;
        }

        public IEnumerable<SavedProvider> GetSaved(int CustomerId)
        {
            var customer = _context.SavedProviders.Where(c => c.CustomerId == CustomerId);
            return customer.ToList();
        }

        public void SaveProviders(int CustomerId, int ProviderId)
        {
            var save =new SavedProvider() { CustomerId = CustomerId, ProviderId = ProviderId };
            _context.SavedProviders.Add(save);
            _context.SaveChanges();
        }

        public void SaveRequest(Requests requests)
        {
            _context.Requests.Add(requests);
            _context.SaveChanges();
        }

        public void UnSaveProviders(int CustomerId, int ProviderId)
        {
            var save = new SavedProvider() { CustomerId = CustomerId, ProviderId = ProviderId };
            _context.SavedProviders.Remove(save);
            _context.SaveChanges();
        }
    }
}
