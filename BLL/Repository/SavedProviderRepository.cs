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
    public class SavedProviderRepository: GenericRepository<SavedProvider>, ISavedProviderRepository
    {
        private readonly ApplicationDbContext _app;

        public SavedProviderRepository(ApplicationDbContext app) : base(app)
        {
            _app = app;
        }
        public List<SavedProvider> SavedProviderWithProviderwithService()
        {
            var savedProviders = _app.SavedProviders
                .Include(s => s.Provider)
                .ThenInclude(p=> p.ApplicationUser)
                .Include(s => s.Provider)
                .ThenInclude(p => p.Service)
                .ToList();
            return savedProviders;
        }
    }

}
