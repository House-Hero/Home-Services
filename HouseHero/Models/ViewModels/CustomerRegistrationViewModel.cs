using System.ComponentModel.DataAnnotations;

namespace HouseHero.Models.ViewModels
{
    public class CustomerRegistrationViewModel
    {
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, ErrorMessage = "Name can't be longer than 50 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 100 characters.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Phone Number is required.")]
        [Phone(ErrorMessage = "Invalid phone number format.")]
        [StringLength(15, ErrorMessage = "Phone number can't be longer than 15 characters.")]
        public string PhoneNumber { get; set; }

        [Range(18, 120, ErrorMessage = "Age must be between 18 and 120.")]
        public int? Age { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        [StringLength(200, ErrorMessage = "Address can't be longer than 200 characters.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "City selection is required.")]
        [Display(Name = "City")]
        public int CityId { get; set; }

        [Display(Name = "Profile Picture")]
        [FileExtensions(Extensions = "jpg,jpeg,png", ErrorMessage = "Profile picture must be a JPG, JPEG, or PNG file.")]
        public IFormFile? ProfilePicture { get; set; }
    }
}