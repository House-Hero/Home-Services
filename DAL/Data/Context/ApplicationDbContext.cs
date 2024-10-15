using DAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data.Context
{
    public class ApplicationDbContext: IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {
        public ApplicationDbContext():base()
        {

        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> option) : base(option)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Available_Day> Available_Day { get; set;}
        public DbSet<Provider> Providers { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Portfolio_image> Portfolio_images { get; set; }
        public DbSet<Portfolio_item> Portfolio_items { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Requests> Requests { get; set; }
        public DbSet<Review> Reviews { get; set; }  
        public DbSet<SavedProvider> SavedProviders { get; set; }

    }
}
