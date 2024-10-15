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
    public class GenericRepository<T> : IGenericRepository<T> where T : ModelBase
    {
        private protected readonly ApplicationDbContext _appDbContext;
        public GenericRepository(ApplicationDbContext appDbContext)//Ask CLR To Create Object From ApplicationDbContext
        {
            _appDbContext = appDbContext;
        }
        public T Get(int id)
        {
            var Result = _appDbContext.Set<T>().Find(id);

            return Result?? default(T);
        }

        public IEnumerable<T> GetAll()
        {
            return _appDbContext.Set<T>().AsNoTracking().ToList();
        }

        public int Add(T Entry)
        {
            _appDbContext.Set<T>().Add(Entry);
            return _appDbContext.SaveChanges();
        }

        public int Update(T Entry)
        {
            _appDbContext.Update(Entry);
            return _appDbContext.SaveChanges();
        }

        public int Delete(T Entry)
        {
            _appDbContext.Remove(Entry);
            return _appDbContext.SaveChanges();
        }
    }
}
