using RestaurantWebsite.Core.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace RestaurantWebsite.Persistence.EntityConfigurations
{
    public class SpecialConfiguration : EntityTypeConfiguration<Special>
    {
        public SpecialConfiguration() {
            Property(c => c.Name)
                .HasMaxLength(256)
                .IsRequired();

            Property(c => c.Description)
                .HasMaxLength(500);

            Property(c => c.StartDate)
                .IsRequired();

            Property(c => c.EndDate)
                .IsRequired();

            HasMany(c => c.FoodsOnSpecial)
                .WithMany(f => f.SpecialsItBelongsTo)
                .Map(m =>
                {
                    m.ToTable("SpecialFoods");
                    m.MapLeftKey("SpecialId");
                    m.MapRightKey("FoodId");
                });
        }
    }
}