using DAL.Data.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public static class IdentitySeed
    {
  
        public static async Task SeedIdentityAsync(this IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            string[] rolesNames = { "Admin" , "Customer" , "Provider"};

                foreach ( var role in rolesNames )
                {
                    //check if role exist
                    if(await roleManager.RoleExistsAsync(role))
                    {
                        continue;
                    }
                    await roleManager.CreateAsync(new ApplicationRole { Name  = role });    
                }
                // Seed cities if they don't exist
                var cities = new List<string> { "أسماعيلية", "القاهرة" }; 
                foreach (var cityName in cities)
                {
                    if (!context.Cities.Any(c => c.Name == cityName))
                    {
                        context.Cities.Add(new City { Name = cityName });
                    }
                }
                string[] categoryNames = { "اعطال منزلية", "تشطيبات وترميمات", "صيانة اجهزة", "نظافة" };
                foreach (var categoryName in categoryNames)
                {
                    if (!context.Categories.Any(c => c.Name == categoryName))
                    {
                        context.Categories.Add(new Category { Name = categoryName });
                    }
                }
            await context.SaveChangesAsync();
                // Create admin user if it doesn't exist
                var admin = new ApplicationUser { Email = "admin@HouseHero.com",
                        UserName = "admin@HouseHero" ,
                        Address = "Ismailia Main Street ",
                        CityId = 1 };
                    var existingAdmin = await userManager.FindByEmailAsync(admin.Email);

            if (existingAdmin == null)
            {
                var result = await userManager.CreateAsync(admin, "9910Pe@");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Admin");
                }
                else
                {
                    // Handle creation failure (optional: log or throw an exception)
                    throw new Exception($"Failed to create admin user: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }
            }

        }
    }
}
