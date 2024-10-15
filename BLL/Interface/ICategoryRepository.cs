using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface
{
    public interface ICategoryRepository:IGenericRepository<Category>
    {
        public Category GetCategoryWithServicesAndProviders(int categoryId);
       
    }
}
