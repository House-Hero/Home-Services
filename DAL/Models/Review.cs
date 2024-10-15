using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Review : ModelBase
    {
        public int Id { get; set; }
        public int ProviderId { get; set; }
        public int CustomerId { get; set; }
        public string? Comment { get; set; } = null!;
        public int Rating { get; set; }

        public Provider? Provider { get; set; } 
        public Customer? Customer { get; set; } 

    }
}
