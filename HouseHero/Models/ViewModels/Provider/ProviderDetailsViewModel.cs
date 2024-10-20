using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace HouseHero.Models.ViewModels.Provider
{
    public class ProviderDetailsViewModel
    {
        [MinLength(10, ErrorMessage = "Address must be at least 10 characters long.")]
        public string Address { get; set; }
        [Display(Name = "City")]
        public int CityId { get; set; }
        public string PhoneNumber { get; set; }
        public int? Age { get; set; }
        public string NationalId { get; set; }
        [Display(Name = "Service")]

        public int ServiceId { get; set; }
    }
}