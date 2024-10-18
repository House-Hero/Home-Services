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
    public class CityRepository : GenericRepository<City>, ICityRepository
    {
        private readonly ApplicationDbContext _context;

        public CityRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
