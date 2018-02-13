namespace RestaurantWebsite.Migrations.RestaurantMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeFoodIdRequired : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FoodPictures", "FoodId", "dbo.Foods");
            DropIndex("dbo.FoodPictures", new[] { "FoodId" });
            AlterColumn("dbo.FoodPictures", "FoodId", c => c.Int(nullable: false));
            CreateIndex("dbo.FoodPictures", "FoodId");
            AddForeignKey("dbo.FoodPictures", "FoodId", "dbo.Foods", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FoodPictures", "FoodId", "dbo.Foods");
            DropIndex("dbo.FoodPictures", new[] { "FoodId" });
            AlterColumn("dbo.FoodPictures", "FoodId", c => c.Int());
            CreateIndex("dbo.FoodPictures", "FoodId");
            AddForeignKey("dbo.FoodPictures", "FoodId", "dbo.Foods", "Id");
        }
    }
}
