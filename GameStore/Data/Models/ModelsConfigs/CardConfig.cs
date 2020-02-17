using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace VaporStore.Data.Models.ModelsConfigs
{
    public class CardConfig : IEntityTypeConfiguration<Card>
    {
        public void Configure(EntityTypeBuilder<Card> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Cvc).IsRequired(true);
            builder.Property(p => p.Number).IsRequired(true);
            builder.Property(p => p.Type).IsRequired(true);
            builder.Property(p => p.UserId).IsRequired(true);
          
            builder.HasMany(p => p.Purchases).WithOne(p => p.Card);
        }
    }
}
