using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace VaporStore.Data.Models.ModelsConfigs
{
    public class GameConfig : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).IsRequired(true);
            builder.Property(p => p.Price).IsRequired(true);
            builder.Property(p => p.ReleaseDate).IsRequired(true);
            builder.Property(p => p.DeveloperId).IsRequired(true);
       
            builder.Property(p => p.GenreId).IsRequired(true);
          
            builder.HasMany(p => p.Purchases).WithOne(p => p.Game);
        }
    }
}
