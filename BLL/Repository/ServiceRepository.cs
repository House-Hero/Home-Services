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
    public class ServiceRepository : GenericRepository<Service>, IServiceRepository
    {
         public ServiceRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
