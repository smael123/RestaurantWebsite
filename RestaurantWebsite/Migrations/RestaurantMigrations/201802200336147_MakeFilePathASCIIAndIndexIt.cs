namespace RestaurantWebsite.Migrations.RestaurantMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeFilePathASCIIAndIndexIt : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.FoodPictures", "FilePath", c => c.String(maxLength: 260, unicode: false));
            CreateIndex("dbo.FoodPictures", "FilePath");
        }
        
        public override void Down()
        {
            DropIndex("dbo.FoodPictures", new[] { "FilePath" });
            AlterColumn("dbo.FoodPictures", "FilePath", c => c.String());
        }
    }
}
