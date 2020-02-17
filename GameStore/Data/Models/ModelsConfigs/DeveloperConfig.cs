using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace VaporStore.Data.Models.ModelsConfigs
{
    class DeveloperConfig : IEntityTypeConfiguration<Developer>
    {
        public void Configure(EntityTypeBuilder<Developer> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).IsRequired(true);
            builder.HasMany(p => p.Games).WithOne(p => p.Developer);
            
        }
    }
}
