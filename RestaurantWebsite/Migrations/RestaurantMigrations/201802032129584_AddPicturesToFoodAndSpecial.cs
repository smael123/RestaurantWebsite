namespace RestaurantWebsite.Migrations.RestaurantMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPicturesToFoodAndSpecial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RestaurantPictures",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FilePath = c.String(),
                        Food_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Foods", t => t.Food_Id)
                .Index(t => t.Food_Id);
            
            AddColumn("dbo.Specials", "SpecialPicture_Id", c => c.Int());
            CreateIndex("dbo.Specials", "SpecialPicture_Id");
            AddForeignKey("dbo.Specials", "SpecialPicture_Id", "dbo.RestaurantPictures", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Specials", "SpecialPicture_Id", "dbo.RestaurantPictures");
            DropForeignKey("dbo.RestaurantPictures", "Food_Id", "dbo.Foods");
            DropIndex("dbo.Specials", new[] { "SpecialPicture_Id" });
            DropIndex("dbo.RestaurantPictures", new[] { "Food_Id" });
            DropColumn("dbo.Specials", "SpecialPicture_Id");
            DropTable("dbo.RestaurantPictures");
        }
    }
}
