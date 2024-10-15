using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data.Configurations
{
    internal class ServiceConfigurations : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> S)
        {
            S.HasKey(x => x.Id);

            S.Property(x => x.Name).HasColumnType("nvarchar").HasMaxLength(50);

            S.HasOne(s=>s.Category)
                .WithMany(c=>c.Services)
                .HasForeignKey(s => s.CategoryId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
