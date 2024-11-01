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
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

       

        public Category GetCategoryWithServicesAndProviders(int categoryId)
        {
            return _context.Categories
                .Include(c => c.Services)              // Eager load related services
                .ThenInclude(s => s.Providers)
                .ThenInclude(AU=>AU.ApplicationUser)
                .ThenInclude(C=>C.City)
                .FirstOrDefault(c => c.Id == categoryId);
        }
    }
}