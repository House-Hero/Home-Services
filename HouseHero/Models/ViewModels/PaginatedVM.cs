using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;

namespace DAL.VM
{
    public class PaginatedVM<T>
    {
        public List<T> item {  get; set; }
        public List<Service> Services { get; set; }
        public int pagenum { get; set; }
        public int Pagesize { get; set; }
        public int Totalitem { get; set; }
        public int totalpage => (int)Math.Ceiling((double)Totalitem/ Pagesize);
        public bool Haspreviouspage => pagenum > 1;
        public bool Hasnextpage => pagenum < totalpage;

    }
}
