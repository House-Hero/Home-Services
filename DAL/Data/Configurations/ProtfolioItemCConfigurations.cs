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
    internal class PortfolioItemCConfigurations : IEntityTypeConfiguration<Portfolio_item>
    {
        public void Configure(EntityTypeBuilder<Portfolio_item> P)
        {
            P.HasKey(x => x.Id);
            P.Property(x => x.Bio).HasColumnType("nvarchar").HasMaxLength(500);
            P.Property(x => x.Name).HasColumnType("nvarchar").HasMaxLength(50);

            P.HasMany(p=>p.Portfolio_Image)
                .WithOne(p=>p.Portfolio_Item)
                .HasForeignKey(p=>p.PortfolioId).OnDelete(DeleteBehavior.Cascade); 
        }
    }
}
