using RestaurantWebsite.Core.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace RestaurantWebsite.Persistence.EntityConfigurations
{
    public class ExtraConfiguration : EntityTypeConfiguration<Extra>
    {
        public ExtraConfiguration() {
            Property(c => c.Name)
                .HasMaxLength(256)
                .IsRequired();

            Property(c => c.AddedPrice)
                .IsRequired();
        }
    }
}