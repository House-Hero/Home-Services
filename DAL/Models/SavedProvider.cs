using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class SavedProvider : ModelBase
    {
        public int CustomerId { get; set; }
        public int ProviderId { get; set; }

        public Customer? Customer { get; set; }
        public Provider? Provider { get; set; }
    }
}
