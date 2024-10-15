using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class ApplicationUser :IdentityUser<int>
    {
        public int? Age { get; set; }

        [Display(Name = "Profile Picture")]
        public int? ProfilePicture_ID { get; set; }
        public string Address { get; set; } = null!;

        public int CityId { get; set; }
        public City City { get; set; } = null!;
    }
}
