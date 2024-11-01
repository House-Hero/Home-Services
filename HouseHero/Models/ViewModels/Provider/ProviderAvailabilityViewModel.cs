using DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace HouseHero.Models.ViewModels.Provider
{
    public class ProviderAvailabilityViewModel
    {
        public string Bio { get; set; }

        //[Display(Name = "Profile Picture")]
        ////public int ProfilePicture { get; set; }

        public List<Day> SelectedDays { get; set; }
        public List<TimeSpan> StartTimes { get; set; }
        public List<TimeSpan> EndTimes { get; set; }
    }
}