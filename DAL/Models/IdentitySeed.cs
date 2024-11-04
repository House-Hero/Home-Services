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
            // Seed Categories and Services
            var categoriesWithServices = new Dictionary<string, List<string>>
        {
            { "اعطال منزلية", new List<string> { "كهرباء", "غاز", "سباكة" } },
            { "تشطيبات وترميمات", new List<string> { "نقاشة", "نجارة" } },
            { "صيانة اجهزة", new List<string> { "ثلاجات", "غسالات", "تكيفات" } },
            { "نظافة", new List<string> { "منزل", "سيارات" } }
        };

            foreach (var category in categoriesWithServices)
            {
                // Check if category exists
                var existingCategory = context.Categories.FirstOrDefault(c => c.Name == category.Key);
                if (existingCategory == null)
                {
                    // Add category if it doesn't exist
                    existingCategory = new Category { Name = category.Key };
                    context.Categories.Add(existingCategory);
                    await context.SaveChangesAsync();  // Save to get the CategoryId
                }

                // Add services for this category
                foreach (var serviceName in category.Value)
                {
                    if (!context.Services.Any(s => s.Name == serviceName && s.CategoryId == existingCategory.Id))
                    {
                        context.Services.Add(new Service { Name = serviceName, CategoryId = existingCategory.Id });
                    }
                }
            }

            // Save changes to the database
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
