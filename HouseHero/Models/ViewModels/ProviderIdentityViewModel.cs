using HouseHero.Models.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace HouseHero.Models.ViewModels
{
    public class ProviderIdentityViewModel
    {
        
        [Required(ErrorMessage = "User Name is required.")]
        [StringLength(256, ErrorMessage = "The {0} must be at least {2} and at most {1} characters long.", MinimumLength = 3)]
        [RegularExpression(@"^[a-zA-Z0-9_.-]*$", ErrorMessage = "The username can only contain letters, numbers, underscores, periods, and hyphens.")]
        [Display(Name = "User Name")]
        [UniqeName]
        public string Name { get; set; }

        [UniqeEmail]
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[\W_]).{6,}$",
        ErrorMessage = "The password must be at least 6 characters long," +
            " with at least one uppercase letter, one lowercase letter," +
            " one number, and one special character.")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}

