using DAL.Data.Context;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace HouseHero.Models.Attributes
{
    public class UniqeEmail : ValidationAttribute
    {
        
       
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            // Resolve the ApplicationDbContext from the validationContext
            var _app = (ApplicationDbContext)validationContext.GetService(typeof(ApplicationDbContext));
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return new ValidationResult("Email is required");
            }
            string email = value.ToString();
            var emailExists = _app.Users.Any(c => c.Email == email);

            if (!emailExists)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("The email must be unique");
            }
        }
    }
}
