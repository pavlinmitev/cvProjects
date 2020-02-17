using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace VaporStore.Data.Models.ModelsConfigs
{
    class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Username).IsRequired(true);
            builder.Property(p => p.FullName).IsRequired(true);
            builder.Property(p => p.Email).IsRequired(true);


        }
    }
}
