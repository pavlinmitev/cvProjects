using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace VaporStore.Data.Models.ModelsConfigs
{
    public class PurchaseConfig : IEntityTypeConfiguration<Purchase>
    {
        public void Configure(EntityTypeBuilder<Purchase> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Type).IsRequired(true);
            builder.Property(p => p.ProductKey).IsRequired(true);
            builder.Property(p => p.Date).IsRequired(true);
            builder.Property(p => p.CardId).IsRequired(true);
          
            builder.Property(p => p.GameId).IsRequired(true);
            
        }
    }
}
