using BLL.Interface;
using DAL.Data.Context;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repository
{
    public class ProviderRepository : GenericRepository<Provider>, IProviderRepository
    {
        private readonly ApplicationDbContext _app;

        public ProviderRepository(ApplicationDbContext app):base(app)
        {
            _app = app;
        }
        public Provider GetProviderWithAllRelatedData(int ProviderId)
        {
            if (ProviderId <= 0)
            {
                throw new ArgumentException("ProviderId must be greater than zero.", nameof(ProviderId));
            }
            var provider= _app.Providers
                    .Include(s=>s.Service)
                    .Include(AU => AU.ApplicationUser)
                    .Include(p=>p.Portfolio_Item)
                    .ThenInclude(p=>p.Portfolio_Image)
                    .Include(av=>av.Available_Day)
                    .Include(R=>R.Reviews)
                    .Include(R=>R.Requests)
                    .FirstOrDefault(c => c.Id == ProviderId);

            return provider;


        }
    }
}
