using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration;
using RestaurantWebsite.Core.Domain;

namespace RestaurantWebsite.Persistence.EntityConfigurations
{
    public class FoodConfiguration : EntityTypeConfiguration<Food>
    {
        public FoodConfiguration()
        {
            Property(c => c.Name)
                .HasMaxLength(256)
                .IsRequired();

            HasIndex(c => c.Name)
                .IsUnique();

            //Description
            Property(c => c.Description)
                .HasMaxLength(500);

            //base price
            Property(c => c.BasePrice)
                .IsRequired();

            HasMany(c => c.Extras);


            //Pictures
            HasMany(c => c.FoodPictures);
        }
    }
}