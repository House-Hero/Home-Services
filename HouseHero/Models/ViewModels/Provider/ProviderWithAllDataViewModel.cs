using DAL.Models;
using HouseHero.Models.Attributes;
using System.ComponentModel.DataAnnotations;

namespace HouseHero.Models.ViewModels.Provider
{
    public class ProviderWithAllDataViewModel
    {
        public int ApplactionUserId { get; set; }
        public int ProviderId { get; set; }
        [Display(Name = "Provider Name")]
        [Required(ErrorMessage = "Name is required.")]
        public string ProviderName { get; set; } = null!;
        [UniqeEmail]
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        [Display(Name = "Email Address")]
        public string Email { get; set; } = null!;
        public int? Age { get; set; }
        public string? Bio { get; set; } = null!;

        [Display(Name = "Profile Picture")]
        public string? ProfilePicture_ID { get; set; }
        public IFormFile? Image { get; set; }
        public string Address { get; set; } = null!;
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; } = null!;

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[\W_]).{6,}$",
       ErrorMessage = "The password must be at least 6 characters long," +
           " with at least one uppercase letter, one lowercase letter," +
           " one number, and one special character.")]
        public string Password { get; set; } = null!;
        public int Rating { get; set; }
        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }
        public string ServiceName { get; set; } = null!;
        public bool Save { get; set; }

        public List<Available_Day>? Days { get; set; } = null!;
        public List<Portfolio_item>? Portfolios { get; set; } = null!;
        public List<Review>? Reviews { get; set; }

        // convert from Provider to ViewModel Avoid Mapping in Controller
        public static implicit operator ProviderWithAllDataViewModel(DAL.Models.Provider provider)
        {
            return new ProviderWithAllDataViewModel
            {
                ProviderId = provider.Id,
                ProviderName = provider.ApplicationUser?.UserName ?? string.Empty,
                Email = provider.ApplicationUser?.Email ?? string.Empty,
                Age = provider.ApplicationUser?.Age,
                Bio = provider.Bio,
                ProfilePicture_ID = provider.ApplicationUser?.ProfilePicture_ID,
                Address = provider.ApplicationUser?.Address ?? string.Empty,
                PhoneNumber = provider.ApplicationUser?.PhoneNumber ?? string.Empty,
                Rating = provider.Rating,
                MinPrice = provider.MinPrice,
                MaxPrice = provider.MaxPrice,
                ServiceName = provider.Service?.Name ?? string.Empty,
                Days = provider.Available_Day?.ToList() ?? new List<Available_Day>(),
                Portfolios = provider.Portfolio_Item?.ToList() ?? new List<Portfolio_item>(),
                Save = provider.Saved != null && provider.Saved.Any(),
                Reviews = provider.Reviews?.ToList() ?? new List<Review>()
            };
        }

        // Implicit conversion from ViewModel to Provider entity
        public static implicit operator DAL.Models.Provider(ProviderWithAllDataViewModel viewModel)
        {
            return new DAL.Models.Provider
            {
                Id=viewModel.ProviderId,
                Bio = viewModel.Bio,
                MinPrice = viewModel.MinPrice,
                MaxPrice = viewModel.MaxPrice,
                Available_Day = viewModel.Days,
                Portfolio_Item = viewModel.Portfolios,
                ApplicationUserId=viewModel.ApplactionUserId
            };
        }
        public static implicit operator DAL.Models.ApplicationUser(ProviderWithAllDataViewModel viewModel)
        {
            return new ApplicationUser
            {
                Id=viewModel.ApplactionUserId,
                UserName = viewModel.ProviderName,
                Email = viewModel.Email,
                Age = viewModel.Age ?? 0,
                Address = viewModel.Address,
                PhoneNumber = viewModel.PhoneNumber,
                ProfilePicture_ID = viewModel.ProfilePicture_ID
            };
        }
    }
}
