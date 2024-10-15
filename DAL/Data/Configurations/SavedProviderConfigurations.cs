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
    internal class SavedProviderConfigurations : IEntityTypeConfiguration<SavedProvider>
    {
        public void Configure(EntityTypeBuilder<SavedProvider> S)
        {
            S.HasKey(s=>new {s.ProviderId,s.CustomerId});
            S.HasOne(s=>s.Customer)
                .WithMany(c=>c.SavedProviders)
                .HasForeignKey(s=>s.CustomerId).OnDelete(DeleteBehavior.NoAction);
            S.HasOne(s=>s.Provider)
                .WithMany(p=>p.Saved)
                .HasForeignKey(s=>s.ProviderId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
