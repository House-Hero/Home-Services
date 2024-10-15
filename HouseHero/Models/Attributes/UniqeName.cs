using DAL.Data.Context;
using System.ComponentModel.DataAnnotations;

namespace HouseHero.Models.Attributes
{
    public class UniqeName : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
           
            var _app = (ApplicationDbContext)validationContext.GetService(typeof(ApplicationDbContext));

            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return new ValidationResult("Name is required");
            }

            string Name = value.ToString();

            var NameExists = _app.Users.Any(c => c.UserName == Name);

            if (!NameExists)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("The Name must be unique");
            }
        }
    }
}
