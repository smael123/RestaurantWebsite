namespace RestaurantWebsite.Migrations.RestaurantMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameRestaurantPictureToFoodPicture : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.RestaurantPictures", newName: "FoodPictures");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.FoodPictures", newName: "RestaurantPictures");
        }
    }
}
