using DAL.Models;
using HouseHero.Models.Attributes;

namespace HouseHero.Models.ViewModels.Customer
{
    public class EditCustomerViewModel
    {
        public int CustomerId { get; set; }
        public int ApplicationUserId { get; set; }
        public string CustomerName { get; set; }
        public string Address {  get; set; }
        public int CityID { get; set; }
        public string PhoneNumber { get; set; }
        public int Age {  get; set; }
		[MaxFileSize(2 * 1024 * 1024, ErrorMessage = "File size cannot exceed 2MB.")]
		public IFormFile? Image { get; set; }
        public string? ImageUrl { get; set; }
        public List<City> Cities { get; set; }


        public static implicit operator DAL.Models.Customer(EditCustomerViewModel c)
        {
            return new DAL.Models.Customer
            {
                Id = c.CustomerId,
                ApplicationUserId = c.ApplicationUserId,
            };
        }
        public static implicit operator ApplicationUser(EditCustomerViewModel c)
        {
            return new ApplicationUser
            {
                Id = c.ApplicationUserId,
                Name = c.CustomerName,
                Age=c.Age,
                Address = c.Address,
                PhoneNumber = c.PhoneNumber,
                CityId=c.CityID,
                ProfilePicture_ID=c.ImageUrl,
            };
        }
        public static implicit operator EditCustomerViewModel(DAL.Models.Customer c)
        {
            return new EditCustomerViewModel
            {
                CustomerId=c.Id,
                ApplicationUserId=c.ApplicationUserId,
                CustomerName =c.ApplicationUser.Name,
                CityID=c.ApplicationUser.CityId,
                Address=c.ApplicationUser.Address,
                Age=c.ApplicationUser.Age ?? 0,
                ImageUrl=c.ApplicationUser.ProfilePicture_ID,
                PhoneNumber=c.ApplicationUser.PhoneNumber
            };
        }
    }
}
