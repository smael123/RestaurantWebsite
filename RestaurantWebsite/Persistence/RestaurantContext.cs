using System;
using System.Collections.Generic;
using System.Data.Entity;
using RestaurantWebsite.Core.Domain;
using System.Linq;
using System.Web;
using RestaurantWebsite.Persistence.EntityConfigurations;
using Microsoft.AspNet.Identity.EntityFramework;

namespace RestaurantWebsite.Persistence
{
    public class RestaurantContext : IdentityDbContext<ApplicationUser>
    {
        public RestaurantContext() : base("name=RestaurantContext") 
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public virtual DbSet<Food> Foods { get; set; }
        public virtual DbSet<Extra> Extras { get; set; }
        public virtual DbSet<Special> Specials { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new FoodConfiguration());
            modelBuilder.Configurations.Add(new ExtraConfiguration());
            modelBuilder.Configurations.Add(new SpecialConfiguration());
        }

        public static RestaurantContext Create()
        {
            return new RestaurantContext();
        }
    }
}