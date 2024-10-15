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
    internal class CustomerConfigurations : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> modelBuilder)
        {
            modelBuilder.ToTable("Customers");
            modelBuilder.HasKey(x => x.Id);


            //Review
            modelBuilder.HasMany(C => C.Reviews)
                .WithOne(R => R.Customer)
                .HasForeignKey(R => R.CustomerId);

            //ApplicationUser
            modelBuilder
            .HasOne(c => c.ApplicationUser)
            .WithOne()
            .HasForeignKey<Customer>(c => c.ApplicationUserId)
            .IsRequired();

        }
    } 
}
