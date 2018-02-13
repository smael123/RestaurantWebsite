namespace RestaurantWebsite.Migrations.RestaurantMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveOldTableNameConstraintRestaurantPictures : DbMigration
    {
        public override void Up()
        {
            this.DropForeignKey("dbo.FoodPictures", "FK_dbo.RestaurantPictures_dbo.Foods_Food_Id");
        }
        
        public override void Down()
        {
            this.AddForeignKey("dbo.FoodPictures", "FoodId", "dbo.Food", "Id", false, "FK_dbo.RestaurantPictures_dbo.Foods_Food_Id");
        }
    }
}
