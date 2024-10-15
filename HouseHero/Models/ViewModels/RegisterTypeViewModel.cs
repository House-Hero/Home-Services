using System.ComponentModel.DataAnnotations;

namespace HouseHero.Models.ViewModels
{
    public class RegisterTypeViewModel
    {
        [Required]
        public bool IsProvider {  get; set; }

    }
}