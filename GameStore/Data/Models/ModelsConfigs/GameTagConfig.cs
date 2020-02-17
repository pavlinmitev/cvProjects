using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace VaporStore.Data.Models.ModelsConfigs
{
   public class GameTagConfig : IEntityTypeConfiguration<GameTag>
    {
        public void Configure(EntityTypeBuilder<GameTag> builder)
        {
            builder.HasKey(p=>new {p.GameId,p.TagId });
            builder.HasOne(p => p.Game).WithMany(p => p.GameTags).HasForeignKey(p=>p.GameId);
            builder.HasOne(p => p.Tag).WithMany(p => p.GameTags).HasForeignKey(p=>p.TagId);


        }
    }
}
