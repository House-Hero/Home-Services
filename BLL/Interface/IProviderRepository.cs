using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface
{
    public interface IProviderRepository:IGenericRepository<Provider>
    {
        public Provider GetProviderWithAllRelatedData(int ProviderId);
        public int GetServiceIdForProvider(int ProviderId);
        public void AddReview(Review review);
        public Provider GetProviderByApplicationUserId(int ApplicationUserId);
        public void UpdateProviderApplactionUser(Provider P ,ApplicationUser user);
        public List<Portfolio_item> GetPortfolio(int PortfolioId);
        public void AddPortfolioItem(Portfolio_item Portfolio);
        public void AddPortfolioImage(Portfolio_image Images);


    }
}
