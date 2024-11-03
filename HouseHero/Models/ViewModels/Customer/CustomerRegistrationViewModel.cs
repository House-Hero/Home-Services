using HouseHero.Models.Attributes;
using System.ComponentModel.DataAnnotations;

namespace HouseHero.Models.ViewModels.Customer
{
    public class CustomerRegistrationViewModel
    {
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, ErrorMessage = "Name can't be longer than 50 characters.")]
        [RegularExpression(@"^[a-zA-Z0-9_.-]*$", ErrorMessage = "The username can only contain letters," +
            " numbers, underscores, periods, and hyphens.")]
        [UniqeName]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        [UniqeEmail]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be between" +
            " 6 and 100 characters.")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[\W_]).{6,}$",
        ErrorMessage = "The password must be at least 6 characters long," +
            " with at least one uppercase letter, one lowercase letter," +
            " one number, and one special character.")]
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
        [FileExtensions(Extensions = "jpg,jpeg,png", ErrorMessage = "Profile picture must" +
            " be a JPG, JPEG, or PNG file.")]
		[MaxFileSize(2 * 1024 * 1024, ErrorMessage = "File size cannot exceed 2MB.")]
		public IFormFile? ProfilePicture { get; set; }
    }
}