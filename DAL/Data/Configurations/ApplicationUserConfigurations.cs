using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Data.Configurations
{
    internal class ApplicationUserConfigurations : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> C)
        {

            //City
            C.HasOne(p => p.City)
            .WithMany(c => c.ApplicationUsers)
            .HasForeignKey(p => p.CityId);
        }
    }
}
