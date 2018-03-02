namespace RestaurantWebsite.Migrations.RestaurantMigrations
{
    using RestaurantWebsite.Core.Domain;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<RestaurantWebsite.Persistence.RestaurantContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\RestaurantMigrations";
        }

        protected override void Seed(RestaurantWebsite.Persistence.RestaurantContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            Food[] foods = new Food[] {
                new Food {
                    Name = "Taco",
                    Description = "Comes with your choice of tortilla and filling.",
                    BasePrice = 1.79m,
                    Extras = new Extra[] {
                        new Extra {
                            Name = "Chopped Tampiquena",
                            AddedPrice = 0.96m
                        }
                    },
                    FoodPictures = new FoodPicture[] {
                        new FoodPicture {
                            FilePath = "/Images/Food/Taco.jpg"
                        },
                        new FoodPicture {
                            FilePath = "/Images/Food/Chicken Fajita Taco.jpg"
                        },
                        new FoodPicture {
                            FilePath = "/Images/Food/Chicharron Taco.jpg"
                        }
                    }
                    
                },
                new Food {
                    Name = "Tampiquena Plate",
                    Description = "A delicious peice of tampiquena steak served with beans, tortillas, and onions.",
                    BasePrice = 14.99m,
                    Extras = new Extra[] {
                        new Extra {
                            Name = "Extra Steak",
                            AddedPrice = 6.99m
                        }
                    },
                    FoodPictures = new FoodPicture [] {
                        new FoodPicture {
                            FilePath = "/Images/Food/Tampiquena Plate.jpg"
                        }
                    }
                },
                new Food {
                    Name = "Menudo Bowl",
                    Description = "A mexican delicacy, menudo is a soup with tripe, hominy, and an unforgettable taste. Comes with cliantro and onion.",
                    BasePrice = 5.79m,
                    Extras = new Extra[] {
                        new Extra {
                            Name = "Large Bowl",
                            AddedPrice = 1
                        },
                        new Extra {
                            Name = "Extra Hominy",
                            AddedPrice = 0.99m
                        }
                    },
                    FoodPictures = new FoodPicture [] {
                        new FoodPicture {
                            FilePath = "/Images/Food/Menudo Bowl.jpg"
                        }
                    }
                },
                new Food {
                    Name = "Flauta Plate",
                    Description = "Fried tortillas with your choice of chicken or beef filling. Served with guacamole and sour cream.",
                    BasePrice = 8.99m,
                    FoodPictures = new FoodPicture [] {
                        new FoodPicture {
                            FilePath = "/Images/Food/Flauta Plate.jpg"
                        }
                    }
                }
            };

            context.Foods.AddOrUpdate(c => c.Name, foods);
            context.SaveChanges();

            Special [] specials = new Special[] {
                new Special {
                    Name = "Half Off Tacos",
                    Description = "For a limited time, all tacos are half off. Extras not included.",
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(14),
                    PictureFilePath = "/Images/SpecialImages/half-off-taco.jpg",
                    FoodsOnSpecial = new List<Food>() {
                        context.Foods.Single(c => c.Name == "Taco")
                    }
                },
                new Special {
                    Name = "Free Taco with purchase of Tampiquena Plate",
                    Description = "For a limited time when you order a tampiquena plate you will get a free taco with your plate.",
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(7),
                    PictureFilePath = "/Images/SpecialImages/free-taco-with-tampiquena.jpg",
                    FoodsOnSpecial = new List<Food>() {
                        context.Foods.Single(c => c.Name == "Taco"),
                        context.Foods.Single(c => c.Name == "Tampiquena Plate")
                    }
                }
            };

            context.Specials.AddOrUpdate(c => c.Name, specials);
            context.SaveChanges();
        }
    }
}
