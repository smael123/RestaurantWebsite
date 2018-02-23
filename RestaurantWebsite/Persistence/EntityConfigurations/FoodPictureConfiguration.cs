using RestaurantWebsite.Core.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace RestaurantWebsite.Persistence.EntityConfigurations
{
    public class FoodPictureConfiguration : EntityTypeConfiguration<FoodPicture>
    {
        public FoodPictureConfiguration() {
            Property(c => c.FilePath)
                .IsUnicode(false)
                .HasMaxLength(260)
                .IsRequired();

            HasIndex(c => c.FilePath);
        }
    }
}