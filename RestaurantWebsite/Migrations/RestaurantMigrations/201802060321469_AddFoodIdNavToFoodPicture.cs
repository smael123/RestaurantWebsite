namespace RestaurantWebsite.Migrations.RestaurantMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFoodIdNavToFoodPicture : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.FoodPictures", name: "Food_Id", newName: "FoodId");
            RenameIndex(table: "dbo.FoodPictures", name: "IX_Food_Id", newName: "IX_FoodId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.FoodPictures", name: "IX_FoodId", newName: "IX_Food_Id");
            RenameColumn(table: "dbo.FoodPictures", name: "FoodId", newName: "Food_Id");
        }
    }
}
