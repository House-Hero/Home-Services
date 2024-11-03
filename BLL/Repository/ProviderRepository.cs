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
            var provider = _app.Providers
                .Include(p => p.Service)
                .Include(p => p.ApplicationUser)
                .Include(p => p.Portfolio_Item)
                .ThenInclude(p => p.Portfolio_Image)
                .Include(p => p.Available_Day)
                .Include(p => p.Requests)
                .Include(p => p.Reviews)
                .ThenInclude(r => r.Customer) // Include Customer here
                .ThenInclude(c => c.ApplicationUser) // Include ApplicationUser here
                .FirstOrDefault(c => c.Id == ProviderId);

                if (provider?.Reviews != null && provider.Reviews.Any())
                {
                  provider.Rating = (int)Math.Round(provider.Reviews.Average(r => r.Rating), MidpointRounding.AwayFromZero);
                }
                else
                {
                    provider.Rating = 0; 
                }
            return provider;
        }
        public int GetServiceIdForProvider(int ProviderId)
        {
            if (ProviderId <= 0)
            {
                throw new ArgumentException("ProviderId must be greater than zero.", nameof(ProviderId));
            }
            int serviceId = (int) _app.Providers.FirstOrDefault(c => c.Id == ProviderId).ServiceId;
            return serviceId;
        }


        public void AddReview(Review review)
        {
            _app.Reviews.Add(review);
            _app.SaveChanges();
        }
        public Provider GetProviderByApplicationUserId(int ApplicationUserId)
        {
            var provider = _app.Providers
                   .FirstOrDefault(c => c.ApplicationUserId == ApplicationUserId);
            return provider;
        }

        public void UpdateProviderApplactionUser(Provider P, ApplicationUser user)
        {
            if (user is not null)
            {
                var result = _app.Users.AsNoTracking().Where(c => c.Id == user.Id).FirstOrDefault();
                if (result != null)
                {
                    
                    result.ProfilePicture_ID = user.ProfilePicture_ID;
                    result.Name = user.Name;
                    result.Age = user.Age;
                    result.Address = user.Address;
                    result.PhoneNumber = user.PhoneNumber;
                    _app.Users.Update(result);
                    _app.Providers.Update(P);
                    _app.SaveChanges();
                }
            }
        }

        public List<Portfolio_item> GetPortfolio(int PortfolioId)
        {
            var portfolioItem = _app.Portfolio_items
                                .Include(p => p.Portfolio_Image)
                                .Where(p => p.Id == PortfolioId);
            return portfolioItem.ToList();
        }

        public void AddPortfolioItem(Portfolio_item Portfolio)
        {
            _app.Portfolio_items.Add(Portfolio);
            _app.SaveChanges();
        }
        public void AddPortfolioImage(Portfolio_image image)
        {
            _app.Portfolio_images.Add(image);
            _app.SaveChanges();
        }
    }
}
