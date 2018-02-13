namespace RestaurantWebsite.Migrations.RestaurantMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeReferenceToRestPictureToFilePath : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Specials", "SpecialPicture_Id", "dbo.RestaurantPictures");
            DropIndex("dbo.Specials", new[] { "SpecialPicture_Id" });
            AddColumn("dbo.Specials", "PictureFilePath", c => c.String());
            DropColumn("dbo.Specials", "SpecialPicture_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Specials", "SpecialPicture_Id", c => c.Int());
            DropColumn("dbo.Specials", "PictureFilePath");
            CreateIndex("dbo.Specials", "SpecialPicture_Id");
            AddForeignKey("dbo.Specials", "SpecialPicture_Id", "dbo.RestaurantPictures", "Id");
        }
    }
}
