using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data.Configurations
{
    internal class ProviderConfigurations : IEntityTypeConfiguration<Provider>
    {
        public void Configure(EntityTypeBuilder<Provider> modelBuilder)
        {
            modelBuilder.ToTable("Providers");
            modelBuilder.HasKey(x => x.Id);
            modelBuilder.Property(x => x.Bio).HasColumnType("nvarchar");

            //Service
            modelBuilder.HasOne(p => p.Service)
             .WithMany(s => s.Providers)
             .HasForeignKey(p => p.ServiceId).OnDelete(DeleteBehavior.SetNull);
            //Portfolio_item
            modelBuilder.HasMany(p => p.Portfolio_Item)
             .WithOne(P => P.Provider)
             .HasForeignKey(p => p.ProviderId).OnDelete(DeleteBehavior.Cascade);
            //Review
            modelBuilder.HasMany(p => p.Reviews)
             .WithOne(R => R.Provider)
             .HasForeignKey(R => R.ProviderId).OnDelete(DeleteBehavior.NoAction);


            //ApplicationUser
            modelBuilder
            .HasOne(c => c.ApplicationUser)
            .WithOne()
            .HasForeignKey<Provider>(c => c.ApplicationUserId)
            .IsRequired();



        }
    }
}
